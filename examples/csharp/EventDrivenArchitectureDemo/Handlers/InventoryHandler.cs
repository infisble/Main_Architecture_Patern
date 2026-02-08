using EventDrivenArchitectureDemo.Contracts;
using EventDrivenArchitectureDemo.Infrastructure;

namespace EventDrivenArchitectureDemo.Handlers;

public sealed class InventoryHandler(IEventBus bus)
{
    public void OnOrderPlaced(OrderPlaced order)
    {
        Console.WriteLine($"[Inventory] Reserving {order.Quantity} of {order.Product}");
        bus.Publish(new InventoryReserved(order.OrderId, order.Product));
    }
}
