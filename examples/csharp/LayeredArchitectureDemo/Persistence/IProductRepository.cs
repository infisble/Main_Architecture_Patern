using LayeredArchitectureDemo.Domain;

namespace LayeredArchitectureDemo.Persistence;

public interface IProductRepository
{
    IReadOnlyList<Product> GetAll();
}
