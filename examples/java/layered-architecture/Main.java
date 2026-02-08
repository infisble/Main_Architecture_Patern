import java.util.ArrayList;
import java.util.Comparator;
import java.util.List;

public class Main {
    record Product(int id, String name, double price) {}

    static class ProductStore {
        private final List<Product> products = List.of(
            new Product(1, "Laptop", 1499.99),
            new Product(2, "Mouse", 39.90),
            new Product(3, "Keyboard", 89.50)
        );

        List<Product> getProducts() {
            return products;
        }
    }

    interface ProductRepository {
        List<Product> getAll();
    }

    static class InMemoryProductRepository implements ProductRepository {
        private final ProductStore store;

        InMemoryProductRepository(ProductStore store) {
            this.store = store;
        }

        public List<Product> getAll() {
            return new ArrayList<>(store.getProducts());
        }
    }

    static class ProductService {
        private final ProductRepository repository;

        ProductService(ProductRepository repository) {
            this.repository = repository;
        }

        List<Product> getProductsOrderedByPrice() {
            return repository.getAll().stream()
                .sorted(Comparator.comparing(Product::price))
                .toList();
        }
    }

    static class ProductController {
        private final ProductService service;

        ProductController(ProductService service) {
            this.service = service;
        }

        void showCatalog() {
            System.out.println("Java layered architecture demo");
            for (Product product : service.getProductsOrderedByPrice()) {
                System.out.printf("- #%d: %s = %.2f%n", product.id(), product.name(), product.price());
            }
        }
    }

    public static void main(String[] args) {
        ProductStore store = new ProductStore();
        ProductRepository repo = new InMemoryProductRepository(store);
        ProductService service = new ProductService(repo);
        ProductController controller = new ProductController(service);

        controller.showCatalog();
    }
}
