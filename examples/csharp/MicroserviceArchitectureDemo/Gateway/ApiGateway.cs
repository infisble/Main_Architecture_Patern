using MicroserviceArchitectureDemo.Contracts;
using MicroserviceArchitectureDemo.Services;

namespace MicroserviceArchitectureDemo.Gateway;

public sealed class ApiGateway(IUserService userService, IOrderService orderService)
{
    public PlaceOrderResponse RoutePlaceOrder(PlaceOrderRequest request)
    {
        if (!userService.Exists(request.UserId))
        {
            return new PlaceOrderResponse(Guid.Empty, "User not found");
        }

        return orderService.CreateOrder(request);
    }
}
