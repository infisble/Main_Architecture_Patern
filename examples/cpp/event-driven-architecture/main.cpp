#include <functional>
#include <iostream>
#include <string>
#include <vector>

struct OrderPlaced {
    std::string orderId;
    std::string product;
    int quantity;
};

struct InventoryReserved {
    std::string orderId;
    std::string product;
};

class EventBus {
public:
    void subscribeOrderPlaced(const std::function<void(const OrderPlaced&)>& handler) {
        orderPlacedHandlers_.push_back(handler);
    }

    void subscribeInventoryReserved(const std::function<void(const InventoryReserved&)>& handler) {
        inventoryReservedHandlers_.push_back(handler);
    }

    void publish(const OrderPlaced& event) const {
        for (const auto& handler : orderPlacedHandlers_) {
            handler(event);
        }
    }

    void publish(const InventoryReserved& event) const {
        for (const auto& handler : inventoryReservedHandlers_) {
            handler(event);
        }
    }

private:
    std::vector<std::function<void(const OrderPlaced&)>> orderPlacedHandlers_;
    std::vector<std::function<void(const InventoryReserved&)>> inventoryReservedHandlers_;
};

class InventoryHandler {
public:
    explicit InventoryHandler(const EventBus& bus) : bus_(bus) {}

    void onOrderPlaced(const OrderPlaced& event) const {
        std::cout << "[Inventory] Reserving " << event.quantity << " of " << event.product << "\n";
        bus_.publish(InventoryReserved{event.orderId, event.product});
    }

private:
    const EventBus& bus_;
};

class NotificationHandler {
public:
    void onOrderPlaced(const OrderPlaced& event) const {
        std::cout << "[Notification] Order accepted: " << event.orderId << "\n";
    }

    void onInventoryReserved(const InventoryReserved& event) const {
        std::cout << "[Notification] Inventory reserved for " << event.product
                  << " (" << event.orderId << ")\n";
    }
};

int main() {
    EventBus bus;
    InventoryHandler inventory(bus);
    NotificationHandler notification;

    bus.subscribeOrderPlaced([&](const OrderPlaced& event) { inventory.onOrderPlaced(event); });
    bus.subscribeOrderPlaced([&](const OrderPlaced& event) { notification.onOrderPlaced(event); });
    bus.subscribeInventoryReserved([&](const InventoryReserved& event) { notification.onInventoryReserved(event); });

    std::cout << "C++ event-driven architecture demo\n";
    bus.publish(OrderPlaced{"ORD-1001", "USB-C Hub", 3});
    return 0;
}
