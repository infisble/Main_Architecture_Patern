using HexagonalArchitectureDemo.Application.Ports;

namespace HexagonalArchitectureDemo.Adapters;

public sealed class ConsoleNotificationAdapter : INotificationPort
{
    public void Notify(string message)
    {
        Console.WriteLine($"[NotificationAdapter] {message}");
    }
}
