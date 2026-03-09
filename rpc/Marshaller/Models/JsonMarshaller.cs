using rpc.Marshaller.Interfaces;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.Json;

namespace rpc.Marshaller.Models;

public class JsonMarshaller : IMarshaller
{
    public  byte[] Marshal<T>(T message)
    {
        var payload = JsonSerializer.Serialize(message);
        return Encoding.UTF8.GetBytes(payload);
        
    }

    public  T UnMarshal<T>(byte[] data)
    {
        var json = Encoding.UTF8.GetString(data);
        return JsonSerializer.Deserialize<T>(json);
    }
}