import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;
import java.util.UUID;
import java.util.function.Consumer;

public class Main {
    record OrderPlaced(UUID orderId, String product, int quantity) {}
    record InventoryReserved(UUID orderId, String product) {}

    static class InMemoryEventBus {
        private final Map<Class<?>, List<Consumer<Object>>> handlers = new HashMap<>();

        <T> void subscribe(Class<T> eventType, Consumer<T> handler) {
            handlers.computeIfAbsent(eventType, key -> new ArrayList<>())
                .add(event -> handler.accept(eventType.cast(event)));
        }

        <T> void publish(T event) {
            List<Consumer<Object>> subscribers = handlers.get(event.getClass());
            if (subscribers == null) {
                return;
            }
            for (Consumer<Object> subscriber : subscribers) {
                subscriber.accept(event);
            }
        }
    }

    static class InventoryHandler {
        private final InMemoryEventBus bus;

        InventoryHandler(InMemoryEventBus bus) {
            this.bus = bus;
        }

        void onOrderPlaced(OrderPlaced event) {
            System.out.println("[Inventory] Reserving " + event.quantity() + " of " + event.product());
            bus.publish(new InventoryReserved(event.orderId(), event.product()));
        }
    }

    static class NotificationHandler {
        void onOrderPlaced(OrderPlaced event) {
            System.out.println("[Notification] Order accepted: " + event.orderId());
        }

        void onInventoryReserved(InventoryReserved event) {
            System.out.println("[Notification] Inventory reserved for " + event.product() + " (" + event.orderId() + ")");
        }
    }

    public static void main(String[] args) {
        InMemoryEventBus bus = new InMemoryEventBus();
        InventoryHandler inventory = new InventoryHandler(bus);
        NotificationHandler notification = new NotificationHandler();

        bus.subscribe(OrderPlaced.class, inventory::onOrderPlaced);
        bus.subscribe(OrderPlaced.class, notification::onOrderPlaced);
        bus.subscribe(InventoryReserved.class, notification::onInventoryReserved);

        System.out.println("Java event-driven architecture demo");
        bus.publish(new OrderPlaced(UUID.randomUUID(), "USB-C Hub", 3));
    }
}
