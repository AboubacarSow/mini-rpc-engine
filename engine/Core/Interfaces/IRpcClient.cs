using engine.Protocol;
using System.Net;

namespace engine.Core.Interfaces;

public interface IRpcClient
{
    public Task<RpcResponse> CallAsync(RpcRequest request,
    CancellationToken cancellationToken = default);
}
