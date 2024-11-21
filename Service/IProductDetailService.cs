using Crud.Api.Entities;

namespace Crud.Api.Service;

public interface IProductDetailService
{
    Task<ProductDetail?> GetDetailsByProductIdAsync(Guid productId);
    Task<ProductDetail> CreateProductDetailAsync(Guid productId, ProductDetail detail);
    Task<ProductDetail> UpdateProductDetailAsync(Guid productId, ProductDetail detail);
    Task<bool> DeleteProductDetailAsync(Guid productId);
}
