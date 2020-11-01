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
    public class RecordGrain :  IActor
    {
        private ILogger<RecordGrain> _logger;

        private uint _recordCount = 0;

        private DateTimeOffset _recordStart;

        private string _serviceName;

        private string _metric;

        //private FasterKV<string, AttributeLog> _kv;
        private FasterLog _log;
        static readonly object _lock = new object();
        
        public RecordGrain(ILogger<RecordGrain> logger)
        {
            _logger = logger;
           
            
        }

     
/*
        public async override Task OnDeactivateAsync()
        {
            string primaryKey = this.GetPrimaryKeyString();
            _log.Commit(true);
            Console.WriteLine($"Ending {_serviceName} - {_recordStart:g} - {_metric}");
            await base.OnDeactivateAsync();
        }*/

        public async Task AddRecord(Record record)
        {
            _recordCount++;
            var offset = (long) record.Time - _recordStart.Ticks;
            foreach (var kvp in record.Attributes)
            {
                var entry = new LogEntry()
                {
                    Offset = (uint) offset,
                    MetricValue = record.Metricvalue,
                    KeyName = kvp.Key,
                    KeyValue = kvp.Value,
                    RecordId = _recordCount
                };
                var bytes = entry.ToByteArray();
                await _log.EnqueueAsync(bytes);
            }

            if (_recordCount % 10_000 == 0)
            {
                _log.Commit();
                Console.WriteLine($"{_recordCount} {_recordStart.Ticks}");
            }

        }

        public async Task ReceiveAsync(IContext context)
        {
           var selfId = context.Self;
           if (_serviceName == null)
           {
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

           if (context.Message is Record msg)
           {
               await AddRecord(msg);
           }
           else if (context.Message is ReceiveTimeout timeout)
           {
               _log.Commit(true);
               Debug.WriteLine($"Committing and Stopping {_serviceName} - {_recordStart:g} - {_metric}");
               context.Stop(context.Self);
               _log = null;
           }
        }
    }
}