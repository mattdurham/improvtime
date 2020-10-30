using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Orleans;
using Orleans.Concurrency;
using ProtoBuf;
using StringDB;
using StringDB.Fluency;
using StringDB.IO;

namespace ImprovTime.Query
{
    [StatelessWorker(1_000)]
    public class QueryMapGrain : Grain, IQueryMapGrain
    {
        public async Task<RecordQueryResult> QueryIndividual(RecordQuery query)
        {
            var f = $"/users/mdurham/improv/{query.ServiceName}.{query.MetricName}.{query.EntryMinute.Ticks}.db";
            var fInfo = new FileInfo(f);
            if (!fInfo.Exists)
            {
                return new RecordQueryResult()
                {
                    Result = 0,
                    Source = query
                };
            }
            using var db  = new DatabaseBuilder()
                .UseIODatabase(StringDBVersion.Latest,f , out var optimalTokenSource)
                .AsReadOnly()
                .WithBuffer(1000)
                .WithTransform(StringDB.Transformers.StringTransformer.Default, StringDB.Transformers.NoneTransformer<byte[]>.Default);
            var queryKeys = query.Attributes.Keys.ToList();
            var logs = new List<LogEntry>();
            foreach (var item in db.EnumerateOptimally(new OptimalTokenSource()))
            {
                // This would be 'Verb'
                if (queryKeys.Contains(item.Key))
                {
                    await using var m = new MemoryStream(item.Value);
                    var entry = Serializer.Deserialize<LogEntry>(m);
                    // This would ensure it was `GET` and falls within the timezone
                    if (entry.KeyValue == query.Attributes[entry.KeyName] 
                        && query.EntryMinute.AddTicks(entry.Offset) >= query.Start
                        && query.EntryMinute.AddTicks(entry.Offset) <= query.End)
                    {
                        logs.Add(entry);
                    }
                }
                else if (queryKeys.Count == 0)
                {
                    await using var m = new MemoryStream(item.Value);
                    var entry = Serializer.Deserialize<LogEntry>(m);
                    if ( query.EntryMinute.AddTicks(entry.Offset) >= query.Start
                        && query.EntryMinute.AddTicks(entry.Offset) <= query.End)
                    {
                        logs.Add(entry);
                    }

                }
            }
            
            var allMatching = (
                from x in logs 
                group  x by x.RecordID into record 
                select record);
            if (query.Aggregate == Aggregate.Count)
            {
                // If there are more records by recordid than we asked for then we know it matches.
                //    Note this is an implicit AND between attributes
                var count = (from x in allMatching where x.Count() >= query.Attributes.Count select x.Key).Count();
                return new RecordQueryResult()
                {
                    Result = count,
                    Source = query
                };
            }

            if (query.Aggregate == Aggregate.Sum)
            {
                // If there are more records by recordid than we asked for then we know it matches.
                //    Note this is an implicit AND between attributes
                var matching = (from x in allMatching where x.Count() >= query.Attributes.Count select x.ToList());
                double sum = 0;
                // Overflow could get real fun here
                foreach (var match in matching)
                {
                    sum += match.Sum(x => x.MetricValue);
                }
                return new RecordQueryResult()
                {
                    Result = sum,
                    Source = query
                };
            }

            return null;
        }
    }
}