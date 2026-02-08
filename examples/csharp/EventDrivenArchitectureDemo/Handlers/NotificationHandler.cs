using EventDrivenArchitectureDemo.Contracts;

namespace EventDrivenArchitectureDemo.Handlers;

public sealed class NotificationHandler
{
    public void OnOrderPlaced(OrderPlaced order)
    {
        Console.WriteLine($"[Notification] Order accepted: {order.OrderId}");
    }

    public void OnInventoryReserved(InventoryReserved inventory)
    {
        Console.WriteLine($"[Notification] Inventory reserved for {inventory.Product} ({inventory.OrderId})");
    }
}
