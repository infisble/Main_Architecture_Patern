using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ClientServerArchitectureDemo.Networking;

public sealed class TcpTextServer(int port)
{
    public async Task StartAsync(int maxClients, CancellationToken cancellationToken)
    {
        var listener = new TcpListener(IPAddress.Loopback, port);
        listener.Start();

        Console.WriteLine($"[Server] Listening on 127.0.0.1:{port}");

        try
        {
            for (var i = 0; i < maxClients; i++)
            {
                using var client = await listener.AcceptTcpClientAsync(cancellationToken);
                await HandleClientAsync(client, cancellationToken);
            }
        }
        finally
        {
            listener.Stop();
            Console.WriteLine("[Server] Stopped");
        }
    }

    private static async Task HandleClientAsync(TcpClient client, CancellationToken cancellationToken)
    {
        await using var stream = client.GetStream();

        var buffer = new byte[1024];
        var read = await stream.ReadAsync(buffer, cancellationToken);
        var message = Encoding.UTF8.GetString(buffer, 0, read);

        Console.WriteLine($"[Server] Received: {message}");

        var response = Encoding.UTF8.GetBytes($"ACK: {message.ToUpperInvariant()}");
        await stream.WriteAsync(response, cancellationToken);
    }
}
