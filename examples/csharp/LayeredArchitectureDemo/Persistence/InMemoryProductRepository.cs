using LayeredArchitectureDemo.Data;
using LayeredArchitectureDemo.Domain;

namespace LayeredArchitectureDemo.Persistence;

public sealed class InMemoryProductRepository(ProductStore store) : IProductRepository
{
    public IReadOnlyList<Product> GetAll() => store.Products;
}
