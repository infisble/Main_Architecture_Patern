#include <iostream>
#include <set>
#include <string>
#include <vector>

struct PlaceOrderRequest {
    int userId;
    std::string product;
    int quantity;
};

struct PlaceOrderResponse {
    std::string orderId;
    std::string status;
};

class UserService {
public:
    bool exists(int userId) const {
        return users_.count(userId) > 0;
    }

private:
    std::set<int> users_{100, 101, 102};
};

class OrderService {
public:
    PlaceOrderResponse createOrder(const PlaceOrderRequest& request) {
        ++counter_;
        PlaceOrderResponse response{
            "ORD-" + std::to_string(counter_),
            "Created for " + request.product + " x" + std::to_string(request.quantity)
        };
        orders_.push_back(response);
        return response;
    }

private:
    int counter_{0};
    std::vector<PlaceOrderResponse> orders_;
};

class ApiGateway {
public:
    ApiGateway(const UserService& userService, OrderService& orderService)
        : userService_(userService), orderService_(orderService) {}

    PlaceOrderResponse routePlaceOrder(const PlaceOrderRequest& request) {
        if (!userService_.exists(request.userId)) {
            return {"", "User not found"};
        }
        return orderService_.createOrder(request);
    }

private:
    const UserService& userService_;
    OrderService& orderService_;
};

int main() {
    UserService userService;
    OrderService orderService;
    ApiGateway gateway(userService, orderService);

    auto good = gateway.routePlaceOrder({100, "Mechanical Keyboard", 2});
    auto bad = gateway.routePlaceOrder({999, "Monitor", 1});

    std::cout << "C++ microservice architecture demo\n";
    std::cout << "OrderId: " << good.orderId << "\n";
    std::cout << "Status : " << good.status << "\n";
    std::cout << "Rejected: " << bad.status << "\n";
    return 0;
}
