using LayeredArchitectureDemo.Domain;
using LayeredArchitectureDemo.Persistence;

namespace LayeredArchitectureDemo.Business;

public sealed class ProductService(IProductRepository repository)
{
    public IReadOnlyList<Product> GetProductsOrderedByPrice()
    {
        return repository
            .GetAll()
            .OrderBy(p => p.Price)
            .ToList();
    }
}
