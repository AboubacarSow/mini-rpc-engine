using System.Net.Sockets;

namespace engine.Transport;

public class FramingProtocol
{
    public static async Task WriteAsync(byte[] message,NetworkStream stream)
    {
        byte[] lengthPrefix = BitConverter.GetBytes(message.Length);

        await stream.WriteAsync(lengthPrefix.AsMemory(0, 4));
        await stream.WriteAsync(message);
    }

    public static async Task<byte[]> ReadAsync(NetworkStream stream)
    {
        byte [] lengthPrefixBuffer = new byte[4];
        await stream.ReadExactlyAsync(lengthPrefixBuffer,0, 4);
        int length = BitConverter.ToInt32(lengthPrefixBuffer,0);
        byte [] messageBuffer = new byte[length];

        await stream.ReadExactlyAsync(messageBuffer,0,length);

        return messageBuffer;
    }


}
