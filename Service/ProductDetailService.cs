using Crud.Api.Entities;  
using Microsoft.EntityFrameworkCore;  
using Microsoft.Extensions.Logging;  

namespace Crud.Api.Service;  

public class ProductDetailService(AppDbContext dbContext, ILogger<ProductDetailService> logger) : IProductDetailService  
{  

    public async Task<ProductDetail?> GetDetailsByProductIdAsync(Guid productId)  
    {  
        logger.LogInformation("Mahsulot tafsilotlarini olish: productId - {ProductId}", productId);  

        var detail = await dbContext.ProductDetails!.FirstOrDefaultAsync(d => d.ProductId == productId);  
        if (detail == null)  
        {  
            logger.LogWarning("Mahsulot tafsilotlari topilmadi: productId - {ProductId}", productId);  
        }  

        return detail;  
    }  

    public async Task<ProductDetail> CreateProductDetailAsync(Guid productId, ProductDetail detail)  
    {  
        detail.Id = Guid.NewGuid();  
        detail.ProductId = productId;  

        logger.LogInformation("Mahsulot tafsilotlarini yaratish: productId - {ProductId}", productId);  
        
        dbContext.ProductDetails!.Add(detail);  
        await dbContext.SaveChangesAsync();  

        logger.LogInformation("Mahsulot tafsilotlari muvaffaqiyatli yaratildi: DetailId - {DetailId}", detail.Id);  
        return detail;  
    }  

    public async Task<ProductDetail> UpdateProductDetailAsync(Guid productId, ProductDetail detail)  
    {  
        logger.LogInformation("Mahsulot tafsilotlarini yangilash: productId - {ProductId}", productId);  

        var existingDetail = await dbContext.ProductDetails!.FirstOrDefaultAsync(d => d.ProductId == productId);  
        if (existingDetail == null)  
        {  
            logger.LogWarning("Mahsulot tafsilotlarini yangilashda xato; topilmadi: productId - {ProductId}", productId);  
            return null!;  
        }  

        existingDetail.Description = detail.Description;  
        existingDetail.Color = detail.Color;  
        existingDetail.Material = detail.Material;  
        existingDetail.Weight = detail.Weight;  
        existingDetail.QuantityInStock = detail.QuantityInStock;  
        existingDetail.ManufactureDate = detail.ManufactureDate;  
        existingDetail.ExpiryDate = detail.ExpiryDate;  
        existingDetail.Size = detail.Size;  
        existingDetail.Manufacturer = detail.Manufacturer;  
        existingDetail.CountryOfOrigin = detail.CountryOfOrigin;  

        await dbContext.SaveChangesAsync();  
        logger.LogInformation("Mahsulot tafsilotlari muvaffaqiyatli yangilandi: productId - {ProductId}", productId);  

        return existingDetail;  
    }  

    public async Task<bool> DeleteProductDetailAsync(Guid productId)  
    {  
        logger.LogInformation("Mahsulot tafsilotlarini o'chirish: productId - {ProductId}", productId);  

        var detail = await dbContext.ProductDetails!.FirstOrDefaultAsync(d => d.ProductId == productId);  
        if (detail == null)  
        {  
            logger.LogWarning("Mahsulot tafsilotlarini o'chirishda xato; topilmadi: productId - {ProductId}", productId);  
            return false;  
        }  

        dbContext.ProductDetails!.Remove(detail);  
        await dbContext.SaveChangesAsync();  
        logger.LogInformation("Mahsulot tafsilotlari muvaffaqiyatli o'chirildi: productId - {ProductId}", productId);  

        return true;  
    }  
}