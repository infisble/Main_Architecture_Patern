using MicroserviceArchitectureDemo.Contracts;
using MicroserviceArchitectureDemo.Gateway;
using MicroserviceArchitectureDemo.Services;

IUserService userService = new UserService();
IOrderService orderService = new OrderService();
var gateway = new ApiGateway(userService, orderService);

var goodRequest = new PlaceOrderRequest(100, "Mechanical Keyboard", 2);
var goodResponse = gateway.RoutePlaceOrder(goodRequest);

Console.WriteLine("Microservice architecture demo");
Console.WriteLine($"OrderId: {goodResponse.OrderId}");
Console.WriteLine($"Status : {goodResponse.Status}");

var badRequest = new PlaceOrderRequest(999, "Monitor", 1);
var badResponse = gateway.RoutePlaceOrder(badRequest);
Console.WriteLine($"Rejected: {badResponse.Status}");
