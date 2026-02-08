using MicroserviceArchitectureDemo.Contracts;

namespace MicroserviceArchitectureDemo.Services;

public interface IOrderService
{
    PlaceOrderResponse CreateOrder(PlaceOrderRequest request);
    IReadOnlyList<PlaceOrderResponse> GetOrders();
}

public sealed class OrderService : IOrderService
{
    private readonly List<PlaceOrderResponse> _orders = [];

    public PlaceOrderResponse CreateOrder(PlaceOrderRequest request)
    {
        var response = new PlaceOrderResponse(Guid.NewGuid(), $"Created for {request.Product} x{request.Quantity}");
        _orders.Add(response);
        return response;
    }

    public IReadOnlyList<PlaceOrderResponse> GetOrders() => _orders;
}
