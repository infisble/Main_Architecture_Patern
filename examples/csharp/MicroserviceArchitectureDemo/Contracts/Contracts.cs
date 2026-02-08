namespace MicroserviceArchitectureDemo.Contracts;

public sealed record PlaceOrderRequest(int UserId, string Product, int Quantity);

public sealed record PlaceOrderResponse(Guid OrderId, string Status);
