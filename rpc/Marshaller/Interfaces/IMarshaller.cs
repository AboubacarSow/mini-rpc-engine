namespace rpc.Marshaller.Interfaces;

public interface IMarshaller
{
    byte[] Marshal<T>(T message);
    T UnMarshal<T>(byte[] data);
}

