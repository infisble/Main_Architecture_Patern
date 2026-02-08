using LayeredArchitectureDemo.Business;

namespace LayeredArchitectureDemo.Presentation;

public sealed class ProductController(ProductService service)
{
    public void ShowCatalog()
    {
        Console.WriteLine("Layered architecture demo: catalog");

        foreach (var product in service.GetProductsOrderedByPrice())
        {
            Console.WriteLine($"- #{product.Id}: {product.Name} = {product.Price:C}");
        }
    }
}
