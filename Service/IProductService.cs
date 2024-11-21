using Crud.Api.Entities;

namespace Crud.Api.Service;

public interface IProductService
{
    IEnumerable<Product> GetAllProducts();
    Task<Product?> GetProductById(Guid id);
    Product CreateProduct(Product product);
    Product UpdateProduct(Guid id, Product product);
    bool DeleteProduct(Guid guid);
}
