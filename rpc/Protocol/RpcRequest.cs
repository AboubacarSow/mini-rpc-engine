namespace rpc.Protocol;

public class RpcRequest
{
    public Guid Id { get; set; }
    public string Method{get;set;}
    public object[] Parameters{get;set;}
    public RpcRequest(string method, object[] parameters)
    {
        Id = Guid.NewGuid();
        Method = method;
        Parameters = parameters;
    }
}
