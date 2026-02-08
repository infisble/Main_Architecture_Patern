using EventDrivenArchitectureDemo.Contracts;
using EventDrivenArchitectureDemo.Handlers;
using EventDrivenArchitectureDemo.Infrastructure;

IEventBus bus = new InMemoryEventBus();
var inventory = new InventoryHandler(bus);
var notification = new NotificationHandler();

bus.Subscribe<OrderPlaced>(inventory.OnOrderPlaced);
bus.Subscribe<OrderPlaced>(notification.OnOrderPlaced);
bus.Subscribe<InventoryReserved>(notification.OnInventoryReserved);

var order = new OrderPlaced(Guid.NewGuid(), "USB-C Hub", 3);
Console.WriteLine("Event-driven architecture demo");
bus.Publish(order);
