using rpc.Protocol;
using rpc.Protocol.Models;
using System.Net;

namespace rpc.Core.Interfaces;

public interface IRpcClient
{
    public Task<RpcResponse> CallAsync(string method,params object[] parameters);
}
