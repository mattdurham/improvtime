using System.Threading.Tasks;
using ImprovTime;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Hosting;

namespace ImprovTimeConsole
{
    class Program
    {
        static async Task Main(string[] args)
        {
            /*var log = Devices.CreateLogDevice(@"/users/mdurham/improv.log");
            var store = new FasterKV<string, AttributeLog>(1L << 20, new LogSettings
            {
                LogDevice = log,
                ObjectLogDevice = log
            });*/
            await new HostBuilder()
                .UseOrleans(builder => builder
                    .UseLocalhostClustering()
                    .ConfigureApplicationParts(_ => _.AddApplicationPart(typeof(RecordGrain).Assembly).WithReferences())
                    .UseDashboard())
                .ConfigureLogging(builder => builder
                    .AddFilter("Orleans.Runtime.Management.ManagementGrain", LogLevel.Warning)
                    .AddFilter("Orleans.Runtime.SiloControl", LogLevel.Warning)
                    .AddConsole())
                .ConfigureServices(s =>
                {
                    //s.AddSingleton(store);
                    //s.AddSingleton(new Committer());

                })
                .RunConsoleAsync();
        }
        
       
    }
}