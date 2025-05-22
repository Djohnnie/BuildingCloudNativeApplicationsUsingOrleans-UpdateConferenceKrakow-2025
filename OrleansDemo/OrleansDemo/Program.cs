using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Orleans.Configuration;
using OrleansDemo.Workers;

IHostBuilder builder = Host.CreateDefaultBuilder(args)
    .UseOrleans(silo =>
    {
        silo.UseLocalhostClustering()
            .ConfigureLogging(logging => logging.AddConsole());
        silo.Configure<GrainCollectionOptions>(o =>
        {
            o.CollectionAge = TimeSpan.FromMinutes(1);
            o.CollectionQuantum = TimeSpan.FromSeconds(10);
        });
    })
    .UseConsoleLifetime();

builder.ConfigureServices(services =>
{
    services.AddHostedService<DemoWorker>();
});

using IHost host = builder.Build();

await host.RunAsync();