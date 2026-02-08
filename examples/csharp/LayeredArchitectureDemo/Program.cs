using LayeredArchitectureDemo.Business;
using LayeredArchitectureDemo.Data;
using LayeredArchitectureDemo.Persistence;
using LayeredArchitectureDemo.Presentation;

var store = new ProductStore();
var repository = new InMemoryProductRepository(store);
var service = new ProductService(repository);
var controller = new ProductController(service);

controller.ShowCatalog();
