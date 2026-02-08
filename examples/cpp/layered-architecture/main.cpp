#include <algorithm>
#include <iostream>
#include <string>
#include <vector>

struct Product {
    int id;
    std::string name;
    double price;
};

class ProductStore {
public:
    std::vector<Product> products() const {
        return {
            {1, "Laptop", 1499.99},
            {2, "Mouse", 39.90},
            {3, "Keyboard", 89.50}
        };
    }
};

class IProductRepository {
public:
    virtual ~IProductRepository() = default;
    virtual std::vector<Product> getAll() const = 0;
};

class InMemoryProductRepository : public IProductRepository {
public:
    explicit InMemoryProductRepository(ProductStore store) : store_(store) {}

    std::vector<Product> getAll() const override {
        return store_.products();
    }

private:
    ProductStore store_;
};

class ProductService {
public:
    explicit ProductService(const IProductRepository& repository) : repository_(repository) {}

    std::vector<Product> getProductsOrderedByPrice() const {
        auto items = repository_.getAll();
        std::sort(items.begin(), items.end(), [](const Product& a, const Product& b) {
            return a.price < b.price;
        });
        return items;
    }

private:
    const IProductRepository& repository_;
};

class ProductController {
public:
    explicit ProductController(const ProductService& service) : service_(service) {}

    void showCatalog() const {
        std::cout << "C++ layered architecture demo\n";
        for (const auto& product : service_.getProductsOrderedByPrice()) {
            std::cout << "- #" << product.id << ": " << product.name << " = " << product.price << "\n";
        }
    }

private:
    const ProductService& service_;
};

int main() {
    ProductStore store;
    InMemoryProductRepository repository(store);
    ProductService service(repository);
    ProductController controller(service);

    controller.showCatalog();
    return 0;
}
