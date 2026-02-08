using LayeredArchitectureDemo.Domain;

namespace LayeredArchitectureDemo.Data;

public sealed class ProductStore
{
    public List<Product> Products { get; } =
    [
        new Product(1, "Laptop", 1499.99m),
        new Product(2, "Mouse", 39.90m),
        new Product(3, "Keyboard", 89.50m)
    ];
}
