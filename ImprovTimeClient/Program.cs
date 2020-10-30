using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ImprovTime;
using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Configuration;

namespace ImprovTimeClient
{
    public class Program
    {
        static int Main(string[] args)
        {
            return RunMainAsync().Result;
        }

        private static async Task<int> RunMainAsync()
        {
            try
            {
                using (var client = await ConnectClient())
                {
                    await DoClientWork(client);
                    Console.ReadKey();
                }

                return 0;
            }
            catch (Exception e)
            {
                Console.WriteLine($"\nException while trying to run client: {e.Message}");
                Console.WriteLine("Make sure the silo the client is trying to connect to is running.");
                Console.WriteLine("\nPress any key to exit.");
                Console.ReadKey();
                return 1;
            }
        }

        private static async Task<IClusterClient> ConnectClient()
        {
            IClusterClient client;
            client = new ClientBuilder()
                .UseLocalhostClustering()
                .Configure<ClusterOptions>(options =>
                {
                    options.ClusterId = "dev";
                    options.ServiceId = "OrleansBasics";
                })
                .ConfigureLogging(logging => logging.AddConsole())
                .Build();

            await client.Connect();
            Console.WriteLine("Client successfully connected to silo host \n");
            return client;
        }

        private static async Task DoClientWork(IClusterClient client)
        {
            var index = 0;
            var verbs = new List<string>()
            {
                "Get",
                "Put",
                "Post"
            };
            var random = new Random();
            
            // example of calling grains from the initialized client
            while (true)
            {
                index++;
                if (index % 1_000 == 0)
                {
                    Console.WriteLine(index);
                }
                var currentTime = DateTimeOffset.Now.ToString("g");
                var metric = "latency";
                var record = client.GetGrain<IRecordGrain>($"test!{currentTime}!{metric}");
                
                await record.AddRecord(new Record()
                {
                    Attributes = new Dictionary<string, string>()
                    {
                        {"Verb", verbs[random.Next(0,2)]}
                    },
                    Service = "test",
                    Time = DateTimeOffset.Now,
                    Value = 10
                });
            }
        }
    }
}