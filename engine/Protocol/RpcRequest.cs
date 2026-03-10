namespace engine.Protocol;

public class RpcRequest
{
    public Guid Id { get; set; }
    public string Method{get;set;}
    public string ServiceName{get;set;}
    public object[] Parameters{get;set;}
    public RpcRequest(string serviceName,string method, object[] parameters)
    {
        Id = Guid.NewGuid();
        ServiceName = serviceName;
        Method = method;
        Parameters = parameters;
    }
}
