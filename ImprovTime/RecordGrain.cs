using System;
using System.IO;
using System.Threading.Tasks;
using ImprovTime.Query;
using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Configuration;
using ProtoBuf;
using StringDB;
using StringDB.Fluency;
using StringDB.IO;


namespace ImprovTime
{
    [CollectionAgeLimit(Days = 0,AlwaysActive = false,Hours = 0,Minutes = 5)]
    public class RecordGrain : Grain , IRecordGrain
    {
        private ILogger<RecordGrain> _logger;

        private uint _recordCount = 0;

        private DateTimeOffset _recordStart;

        private string _serviceName;

        private string _metric;

        private IDatabase<string, byte[]> _database;
        //private FasterLog _log;
        //private AttributeLog _attrs = new AttributeLog();

        //private FasterKV<string, AttributeLog> _kv;
        
        static object _lock = new object();
        public RecordGrain(ILogger<RecordGrain> logger)
        {
            _logger = logger;
        }

        public override async Task OnActivateAsync()
        {
            string primaryKey = this.GetPrimaryKeyString();
            
            var keyParts = primaryKey.Split("!");
            _serviceName = keyParts[0];
            _recordStart = DateTimeOffset.Parse(keyParts[1]);
            _metric = keyParts[2];
            lock (_lock)
            {
                if (!Directory.Exists("./improv"))
                {
                    Directory.CreateDirectory("./improv");
                }
            }

            _database = new DatabaseBuilder()
                .UseIODatabase(StringDBVersion.Latest, $"./improv/{_serviceName}.{_metric}.{_recordStart.Ticks}.db", out var optimalTokenSource)
                .WithBuffer(1000)
                .WithTransform(StringDB.Transformers.StringTransformer.Default, StringDB.Transformers.NoneTransformer<byte[]>.Default);
            Console.WriteLine($"Starting {_serviceName} - {_recordStart:g} - {_metric}");
            await base.OnActivateAsync();
        }

        public async override Task OnDeactivateAsync()
        {
            string primaryKey = this.GetPrimaryKeyString();
            Console.WriteLine($"Ending {_serviceName} - {_recordStart:g} - {_metric}");
            await base.OnDeactivateAsync();
        }
        
        public async Task AddRecord(Record record)
        {
            _recordCount++;
            var offset = record.Time.Ticks - _recordStart.Ticks;
            foreach(var kvp in record.Attributes)
            {
                var entry = new LogEntry()
                {
                    Offset = (uint) offset,
                    MetricValue = record.Value,
                    KeyName = kvp.Key,
                    KeyValue = kvp.Value,
                    RecordID = _recordCount
                };
                await using var mem = new MemoryStream();
                Serializer.Serialize(mem, entry);
                _database.Insert(entry.KeyName, mem.ToArray());
            }

            if (_recordCount % 1_000 == 0)
            {
                Console.WriteLine(_recordCount);
            }
            
        }

        public Task<RecordQueryResult> RunQuery(RecordQuery query)
        {
            return null;
            /*
            var matchingRecords = new List<KeyValuePair<AttrKey, Attr>>();
            
            // Step 1 find all the records
            foreach (var kvp in query.Attributes)
            {
                var foundRecord = _attrs.Where(x => x.Key.AttributeType == kvp.Key && x.Key.AttributeValue == kvp.Value);
                matchingRecords.AddRange(foundRecord);
            }

            double sumAggregate = 0;
            // Step 2 check sum aggregate
            if (query.Aggregate == Aggregate.Count)
            {
                var uniqueIds = new HashSet<uint>();
                matchingRecords.ForEach(x => x.Value.Datums.ForEach(d =>
                {
                    uniqueIds.Add(d.RecordID);
                }));
                return Task.FromResult( new RecordQueryResult
                {
                    Result = uniqueIds.Count,
                    Source = query
                });
            }
            
            // Check for Sum aggregate
            if(query.Aggregate == Aggregate.Sum)
            {
                var uniqueIds = new HashSet<uint>();
                matchingRecords.ForEach(x => x.Value.Datums.ForEach(d =>
                {
                    if(uniqueIds.Add(d.RecordID))
                    {
                        sumAggregate += d.Value;
                    }
                }));
                return Task.FromResult( new RecordQueryResult
                {
                    Result = sumAggregate,
                    Source = query
                });
            }
            return Task.FromResult<RecordQueryResult>(null);*/
        }
        
    
    }
}