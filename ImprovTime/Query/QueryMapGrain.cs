using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FASTER.core;
using LightningDB;


namespace ImprovTime.Query
{
    public class QueryMapGrain 
    {
        public async Task<RecordQueryResult> QueryIndividual(RecordQuery query)
        {
            var ticks = query.EntryMinute.UtcDateTime.Ticks;
            var f =
                $"/Users/mdurham/Source/ImprovTime/ImprovTimeConsole/improv/{query.ServiceName}.{query.MetricName}.{ticks}/hlog.log";
            var fInfo = new FileInfo(f);
            if (fInfo.Directory != null && !fInfo.Directory.Exists)
            {
                return new RecordQueryResult()
                {
                    Result = 0,
                    Source = query
                };
            }

            var kvFile =
                $"/Users/mdurham/Source/ImprovTime/ImprovTimeConsole/improv/{query.ServiceName}.{query.MetricName}.{ticks}.kv";

            // Open up our KeyValue that holds the cached data, since it is immutable one we have calculated the results for a given
            //    query we can save those. In a perfect world they would already be calculated throw some handoff process one the minute passes
            //    for common/know queries
            using var env = new LightningEnvironment(kvFile) {MaxDatabases = 5};
            env.Open();
            using var tx = env.BeginTransaction();
            
            // This is the accessor to the huge amount of data
            using var fasterKvDevice = Devices.CreateLogDevice(kvFile);
            
            
            StringBuilder key = new StringBuilder();
            key.Append(query.ServiceName);
            key.Append(query.MetricName);
            key.Append(query.EntryMinute.Ticks);
            foreach (var kvp in query.Attributes)
            {
                key.Append(kvp.Name + ":" + kvp.Value + ";");
            }

            key.Append(query.Aggregate.ToString());
            var keyBytes = Encoding.UTF8.GetBytes(key.ToString());
            using (var db = tx.OpenDatabase("kv", new DatabaseConfiguration {Flags = DatabaseOpenFlags.Create}))
            {
                // Check the cache to see if its there
                var result = tx.Get(db, keyBytes);
                if (result.resultCode == MDBResultCode.Success)
                {
                    // TODO look at using a span to prevent memcopy
                    var value = BitConverter.ToDouble(result.value.CopyToNewArray(), 0);
                    return new RecordQueryResult()
                    {
                        Result = value,
                        Source = query
                    };
                }
            }

            // Not in cache, gotta hit the raw records
            var device = Devices.CreateLogDevice(f);
            var log = new FasterLog(new FasterLogSettings {LogDevice = device});
            
            // We only support COUNT and SUM at the moment
            double totalCount = 0;
            double sumValue = 0;
            
            using (var iter = log.Scan(log.BeginAddress, long.MaxValue))
            {
                var more = iter.GetNext(out byte[] result, out _, out _,
                    out _);

                var entry = LogEntry.Parser.ParseFrom(result);
                while (more)
                { 
                    if (IsValid(entry, query))
                    {
                        totalCount++;
                        // TODO what about overflows?
                        sumValue += entry.MetricValue;
                    }
                    more = iter.GetNext(out result, out _, out _,
                        out _);
                    // Only slam the next entry if we found one
                    if (more)
                    {
                        entry = LogEntry.Parser.ParseFrom(result);
                    }

                }
            }

            if (query.Aggregate == Aggregate.Count)
            {
                SaveKV(tx, keyBytes, totalCount);
                return new RecordQueryResult()
                {
                    Result = totalCount,
                    Source = query
                };
            }

            if (query.Aggregate == Aggregate.Sum)
            {
                SaveKV(tx, keyBytes, sumValue);
                return new RecordQueryResult()
                {
                    Result = sumValue,
                    Source = query
                };
            }

            return null;
        }

        private void SaveKV(LightningTransaction tx, byte[] key, double value)
        {
            var byteValue = BitConverter.GetBytes(value);
            using var db = tx.OpenDatabase("kv", new DatabaseConfiguration { Flags = DatabaseOpenFlags.Create });
            tx.Put(db, key, byteValue);
            tx.Commit();
        }

        private bool IsValid(LogEntry item,RecordQuery query)
        {
            // If not attributes specific then it matches!
            if (query.Attributes.Count == 0)
            {
                return true;
            }

            var matchedAttributes = (from x in query.Attributes
                where item.Attributes.ContainsKey(x.Name) && item.Attributes[x.Name] == x.Value
                select x).Count();
            return matchedAttributes == query.Attributes.Count;
        }
    }
}