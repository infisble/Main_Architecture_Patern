using ClientServerArchitectureDemo.Networking;

var cancellation = new CancellationTokenSource(TimeSpan.FromSeconds(10));
const int port = 5056;

var server = new TcpTextServer(port);
var serverTask = server.StartAsync(maxClients: 2, cancellation.Token);

await Task.Delay(250, cancellation.Token);

var clientA = new TcpTextClient("127.0.0.1", port);
var clientB = new TcpTextClient("127.0.0.1", port);

var responseA = await clientA.SendAsync("hello from client A", cancellation.Token);
var responseB = await clientB.SendAsync("hello from client B", cancellation.Token);

Console.WriteLine($"[Client A] {responseA}");
Console.WriteLine($"[Client B] {responseB}");

await serverTask;
