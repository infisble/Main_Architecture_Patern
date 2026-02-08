namespace EventDrivenArchitectureDemo.Contracts;

public sealed record OrderPlaced(Guid OrderId, string Product, int Quantity);
public sealed record InventoryReserved(Guid OrderId, string Product);
