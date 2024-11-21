using Crud.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace Crud.Api.Service;

public class ProductService(AppDbContext dbContext, ILogger<ProductService> logger) : IProductService
{
    public Product CreateProduct(Product product)
    {
        product.Id = Guid.NewGuid();
        product.CreatedAt = DateTime.UtcNow;
        product.ModifiedAt = DateTime.UtcNow;
        logger.LogInformation("{product} mahsulotiga id va vaqtlar generatsiya qilinmqoda", product.Name);

        dbContext.Products!.Add(product);
        dbContext.SaveChanges();

        logger.LogInformation("{product.Name} nomli mahsulot databasega saqlandi ", product.Name);
        return product;
    }

    public bool DeleteProduct(Guid id)
    {
        logger.LogInformation("{id} idli mahsulot database dan qidirilmoqda", id);
        var product = dbContext.Products!.Find(id);
        if (product == null)
        {
            logger.LogWarning("O'chirish uchun {id} mahsulot databasedan topilmadi", id);
            return false;
        }


        dbContext.Products.Remove(product);
        dbContext.SaveChanges();
        logger.LogInformation("{id} idli mahsulot databasedan ochirib tashlandi", id);
        return true;
    }

    public IEnumerable<Product> GetAllProducts()
    {
        logger.LogInformation("database barcha mahsulotlari");
        return [.. dbContext.Products!];
    }

    public Task<Product?> GetProductById(Guid id)
    {
        logger.LogInformation("{id} idli mahsulot databasedan qidirilmoq", id);
        return dbContext.Products!.FirstOrDefaultAsync(p => p.Id == id);
    }


    public Product UpdateProduct(Guid id, Product product)
    {
        logger.LogInformation("{id} idli mahsulot databasedan qidirilmoq", id);
        var borProduct = dbContext.Products?.Find(id);
        if (borProduct == null)
        {
            logger.LogWarning("yangilash uchun databaseda {id}idli mahsulot topilmadi",id);
            return null!;
        }

        borProduct.Name = product.Name;
        borProduct.Price = product.Price;
        borProduct.ModifiedAt = DateTime.UtcNow;
        borProduct.Status = product.Status;

        dbContext.SaveChanges();
        logger.LogInformation("{product} nomlimahsulot yangilandi va databasega saqlandi", borProduct.Name);

        return borProduct;
    }
}