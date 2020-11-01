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
            if (!fInfo.Directory.Exists)
            {
                return new RecordQueryResult()
                {
                    Result = 0,
                    Source = query
                };
            }
            var kvFile =
                $"/Users/mdurham/Source/ImprovTime/ImprovTimeConsole/improv/{query.ServiceName}.{query.MetricName}.{ticks}.kv";

            using var env = new LightningEnvironment(kvFile);
            
                env.MaxDatabases = 5;
                env.Open();
                using var tx = env.BeginTransaction();
               
            

            
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
            var keyBytes = UTF8Encoding.UTF8.GetBytes(key.ToString()); 
            using (var db = tx.OpenDatabase("kv", new DatabaseConfiguration { Flags = DatabaseOpenFlags.Create }))
            {
                var result = tx.Get(db,keyBytes);
                if (result.resultCode == MDBResultCode.Success)
                {
                    // TODO look at using a span
                    var value = BitConverter.ToDouble(result.value.CopyToNewArray(), 0); 
                    return new RecordQueryResult()
                    {
                        Result = value,
                        Source = query
                    };
                }
            }

            
            var device = Devices.CreateLogDevice(f);
            var log = new FasterLog(new FasterLogSettings {LogDevice = device});
            // Record Id and Count
            double totalCount = 0;
            double sumValue = 0;
            using (var iter = log.Scan(log.BeginAddress, long.MaxValue))
            {
                var more = iter.GetNext(out byte[] result, out int entryLength, out long currentAddress,
                    out long nextAddress);
                var entry = LogEntry.Parser.ParseFrom(result);
                var matchedAttributes = 0;
                // Set out initial value of old to the same as entry
                LogEntry oldEntry = entry;
                while (more)
                {
                    if (entry.RecordId != oldEntry.RecordId)
                    {
                        if (matchedAttributes >= query.Attributes.Count)
                        {
                            totalCount++;
                            sumValue += oldEntry.MetricValue;
                        }
                        // Reset matched attributes
                        matchedAttributes = 0;
                    }

                    if (IsValid(entry, query))
                    {
                        matchedAttributes++;
                    }

                    more = iter.GetNext(out result, out entryLength, out currentAddress,
                        out nextAddress);
                    oldEntry = entry;
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
            using (var db = tx.OpenDatabase("kv", new DatabaseConfiguration { Flags = DatabaseOpenFlags.Create }))
            {
                tx.Put(db, key, byteValue);
                tx.Commit();
            }
        }

        private bool IsValid(LogEntry item,RecordQuery query)
        {
            if (query.Attributes.Count == 0)
            {
                if ( query.EntryMinute.AddTicks(item.Offset) >= query.Start
                     && query.EntryMinute.AddTicks(item.Offset) <= query.End)
                {
                    return true;
                }

            }

            

            // Does the item match any of the queries, in this case it is an AND and case sensitive (probably should be case insensitive)
            var found = query.Attributes.Any(x => x.Name == item.KeyName && x.Value == item.KeyValue);
            // This would be 'Verb'
            if (found)
            {
                
                if (query.EntryMinute.AddTicks(item.Offset) >= query.Start
                    && query.EntryMinute.AddTicks(item.Offset) <= query.End)
                {
                    return true;
                }
            }
           
            return false;
        }
        
        
    }
}