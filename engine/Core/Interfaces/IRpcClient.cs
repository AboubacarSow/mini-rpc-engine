using engine.Protocol;
using System.Net;

namespace engine.Core.Interfaces;

public interface IRpcClient
{
    public Task<RpcResponse> CallAsync(string serviceName,string method,
    CancellationToken cancellationToken = default,params object[] parameters);
}
