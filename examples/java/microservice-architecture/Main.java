import java.util.ArrayList;
import java.util.HashSet;
import java.util.List;
import java.util.Set;
import java.util.UUID;

public class Main {
    record PlaceOrderRequest(int userId, String product, int quantity) {}
    record PlaceOrderResponse(UUID orderId, String status) {}

    interface UserService {
        boolean exists(int userId);
    }

    static class InMemoryUserService implements UserService {
        private final Set<Integer> users = new HashSet<>(Set.of(100, 101, 102));

        public boolean exists(int userId) {
            return users.contains(userId);
        }
    }

    interface OrderService {
        PlaceOrderResponse createOrder(PlaceOrderRequest request);
        List<PlaceOrderResponse> getOrders();
    }

    static class InMemoryOrderService implements OrderService {
        private final List<PlaceOrderResponse> orders = new ArrayList<>();

        public PlaceOrderResponse createOrder(PlaceOrderRequest request) {
            PlaceOrderResponse response = new PlaceOrderResponse(
                UUID.randomUUID(),
                "Created for " + request.product() + " x" + request.quantity()
            );
            orders.add(response);
            return response;
        }

        public List<PlaceOrderResponse> getOrders() {
            return orders;
        }
    }

    static class ApiGateway {
        private final UserService userService;
        private final OrderService orderService;

        ApiGateway(UserService userService, OrderService orderService) {
            this.userService = userService;
            this.orderService = orderService;
        }

        PlaceOrderResponse placeOrder(PlaceOrderRequest request) {
            if (!userService.exists(request.userId())) {
                return new PlaceOrderResponse(new UUID(0L, 0L), "User not found");
            }
            return orderService.createOrder(request);
        }
    }

    public static void main(String[] args) {
        UserService userService = new InMemoryUserService();
        OrderService orderService = new InMemoryOrderService();
        ApiGateway gateway = new ApiGateway(userService, orderService);

        PlaceOrderResponse good = gateway.placeOrder(new PlaceOrderRequest(100, "Mechanical Keyboard", 2));
        PlaceOrderResponse bad = gateway.placeOrder(new PlaceOrderRequest(999, "Monitor", 1));

        System.out.println("Java microservice architecture demo");
        System.out.println("OrderId: " + good.orderId());
        System.out.println("Status : " + good.status());
        System.out.println("Rejected: " + bad.status());
    }
}
