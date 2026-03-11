using engine.Core;
using engine.Core.Interfaces;
using engine.Marshaller.Models;
using engine.Transport;
using Microsoft.Extensions.DependencyInjection;

namespace engine.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddRpcEngine(this IServiceCollection services)
    {
        services.AddSingleton<IRpcRequestHandler, RpcProxy>();

        services.AddSingleton<TransportListener>();
        services.AddSingleton<JsonMarshaller>();
        services.AddHostedService<RpcServer>();


        return services;
    }

    public static void AddServices(this IServiceCollection services,Action<Register> config)
    {

        var register = new Register();

        config(register);

        services.AddSingleton(register);
    }
}
