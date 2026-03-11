
namespace engine.Protocol;

public class RpcResponse
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public object Data{get;set;}
    public RpcResponse() { }
    public RpcResponse(object message)
    {
        Data = message;
    }
}
