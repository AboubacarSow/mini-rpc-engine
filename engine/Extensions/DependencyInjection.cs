using engine.Core;
using engine.Core.Interfaces;
using engine.Marshaller.Models;
using engine.Transport;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace engine.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddRpcEngine(this IServiceCollection services)
    {
        services.AddSingleton<IRpcRequestHandler, RpcProxy>();

        services.AddSingleton<TransportListener>();
        services.AddSingleton<JsonMarshaller>();


        return services;
    }

    public static void AddServices(this IServiceCollection services,Action<Register> config)
    {
        services.AddSingleton<Register>();

        var register = new Register();

        config(register);
    }
}

public static class Startup
{
    public static async Task StandUp(){
        var builder = Host.CreateDefaultBuilder();

        builder.ConfigureServices(services =>
        {
            services.AddHostedService<RpcServer>();
        });

        builder.Start();

        var host = builder.Build();

        await host.RunAsync();


    }
}