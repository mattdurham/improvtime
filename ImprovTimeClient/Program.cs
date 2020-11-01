using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Google.Protobuf.Collections;
using ImprovTime;
using Proto;
using Proto.Remote;

namespace ImprovTimeClient
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            var system = new ActorSystem();
            var serialization = new Serialization();
            serialization.RegisterFileDescriptor(RecordReflection.Descriptor);
            var remote = new Remote(system, new RemoteConfig()
            {
                Serialization = serialization,
                Host = "127.0.0.1",
                Port = 0,
            });
            await remote.StartAsync();
            var context = new RootContext(system, default);
            await DoClientWork(remote, context);
        }

     
        
        private static async Task DoClientWork(Remote client, IRootContext context)
        {
            var index = 0;
            var verbs = new List<string>()
            {
                "Get",
                "Put",
                "Post"
            };
            var random = new Random();
            var lastKey = "";
            PID pid = null;

            // example of calling grains from the initialized client
            while (true)
            {
                var currentTime = DateTimeOffset.Now.ToString("g");
                var metric = "latency";

                var currentKey = $"test!{currentTime}!{metric}";
                if (currentKey != lastKey)
                {
                    // This can be expensive
                    var result = await client.SpawnNamedAsync("127.0.0.1:8000",currentKey, "record",
                        TimeSpan.FromMinutes(30));
                    pid = result.Pid;
                    lastKey = currentKey;
                }

                index++;
                if (index % 10_000 == 0)
                {
                    System.Threading.Thread.Sleep(100);
                    Console.WriteLine(index);
                }
                
                var r = new Record()
                {
                    Service = "test",
                    Time = (ulong)DateTimeOffset.Now.Ticks,
                    Metricvalue = 10
                };
                r.Attributes.Add("Verb", verbs[random.Next(0, 3)]);
                context.Send(pid,r);
                
            }
        }
    }
}