using System.Net.Sockets;
using System.Text;

namespace ClientServerArchitectureDemo.Networking;

public sealed class TcpTextClient(string host, int port)
{
    public async Task<string> SendAsync(string message, CancellationToken cancellationToken)
    {
        using var client = new TcpClient();
        await client.ConnectAsync(host, port, cancellationToken);

        await using var stream = client.GetStream();
        var payload = Encoding.UTF8.GetBytes(message);
        await stream.WriteAsync(payload, cancellationToken);

        var buffer = new byte[1024];
        var read = await stream.ReadAsync(buffer, cancellationToken);
        return Encoding.UTF8.GetString(buffer, 0, read);
    }
}
