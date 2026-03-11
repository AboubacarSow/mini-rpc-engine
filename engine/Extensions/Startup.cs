using engine.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace engine.Extensions;

public static class Startup
{
    public static async Task ConfigureRpcEngine(){
        var builder = Host.CreateDefaultBuilder();

        builder.ConfigureServices(services =>
        {
            services.AddHostedService<RpcServer>();
        });


        using IHost host = builder.Build();

        await host.RunAsync();


    }
}