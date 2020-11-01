using System;
using System.IO;
using System.Threading.Tasks;
using ImprovTime;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Proto;
using Proto.Mailbox;
using Proto.Remote;


namespace ImprovTimeConsole
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var configuration = SetupConfiguration(args);
            var services = RegisterServices(args);
            
            var system = new ActorSystem();
            var serialization = new Serialization();
            var context = new RootContext(system);
            serialization.RegisterFileDescriptor(RecordReflection.Descriptor);
            var props = Props.FromProducer(() => services.GetService<RecordGrain>())
                .WithMailbox(() => BoundedMailbox.Create(2_000_000));
            var remote = new Remote(system, new RemoteConfig()
            {
                Serialization = serialization,
                Host = "127.0.0.1",
                Port = 8000,
                RemoteKinds = { {"record", props}}
            });
            await remote.StartAsync();
            Console.WriteLine("Server started");
            Console.ReadLine();
        }
        
        private static IConfiguration SetupConfiguration(string[] args)
        {
            return new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true)
                .AddEnvironmentVariables()
                .AddCommandLine(args)
                .Build();
        }
        
        private static ServiceProvider RegisterServices(string[] args)
        {
            IConfiguration configuration = SetupConfiguration(args);
            var serviceCollection = new ServiceCollection();
    
            serviceCollection.AddSingleton(configuration);
            serviceCollection.AddLogging(cfg => cfg.AddConsole());
            serviceCollection.AddSingleton(configuration);
            serviceCollection.AddTransient<RecordGrain>();
            return serviceCollection.BuildServiceProvider();
        }
        
       
    }
}