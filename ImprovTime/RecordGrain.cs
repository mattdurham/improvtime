using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using FASTER.core;
using Google.Protobuf;
using ImprovTime.Query;
using Microsoft.Extensions.Logging;
using Proto;


namespace ImprovTime
{
    /// <summary>
    /// Main ingestion point, represents a minute slice for one service and one metric
    /// </summary>
    public class RecordGrain :  IActor
    {
        private ILogger<RecordGrain> _logger;

        // This is used to keep track of process the record. It only has to be unique within the minute
        private uint _recordCount = 0;

        private DateTimeOffset _recordStart;

        private string _serviceName;

        private string _metric;

        // Our primary datasource, this is a high performance log that allows concurrent reads
        private FasterLog _log;
        
        static readonly object _lock = new object();
        
        public RecordGrain(ILogger<RecordGrain> logger)
        {
            _logger = logger;
        }

        public async Task AddRecord(Record record)
        {
            // Received a record so increment our counter, probably not needed, but if we want to ever know the
            //    order that entries came in, as opposed to their time
            _recordCount++;
            // We don't need to store the whole time, just the offset from the start of the minute
            var offset = (long) record.Time - _recordStart.UtcTicks;
            
            var entry = new LogEntry()
            {
                Offset = (uint) offset,
                MetricValue = record.Metricvalue,
                RecordId = _recordCount,
            };
            foreach (var attr in record.Attributes)
            {
                entry.Attributes.Add(attr.Key,attr.Value);    
            }
            
            var allBytes = entry.ToByteArray();
            await _log.EnqueueAsync(allBytes);
            
            if (_recordCount % 10_000 == 0)
            {
                _log.Commit();
                Console.WriteLine($"{_recordCount} {_recordStart.Ticks}");
            }

        }

        public async Task ReceiveAsync(IContext context)
        {
            if (_serviceName == null)
            {
                HandleStartup(context);
            }

            switch (context.Message)
            {
                case Record msg:
                    await AddRecord(msg);
                    break;
                // This is triggered if no message is processed for 90s
                //    Since we only cover 60 seconds this allows us to spin down this actor
                case ReceiveTimeout timeout:
                    _log.Commit(true);
                    Debug.WriteLine($"Committing and Stopping {_serviceName} - {_recordStart:g} - {_metric}");
                    context.Stop(context.Self);
                    _log = null;
                    break;
            }
        }

        private void HandleStartup(IContext context)
        {
            var selfId = context.Self;
            var primaryKey = selfId.Id;
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
            var device = Devices.CreateLogDevice($"./improv/{_serviceName}.{_metric}.{_recordStart.UtcTicks}/hlog.log");
            _log = new FasterLog(new FasterLogSettings
            {
                LogDevice = device,
            });
            context.SetReceiveTimeout(TimeSpan.FromSeconds(90));
            Debug.WriteLine($"Starting {_serviceName} - {_recordStart:g} - {_metric}");
        }
    }
}