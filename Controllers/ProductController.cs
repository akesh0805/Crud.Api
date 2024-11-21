using Crud.Api.Dtos;
using Crud.Api.Entities;
using Crud.Api.Service;
using Microsoft.AspNetCore.Mvc;

namespace Crud.Api.Controllers;


[ApiController]
[Route("api/[controller]")]
public class ProductController(IProductService productService, ILogger<ProductController> logger) : Controller
{
    [HttpPost]
    public async Task<ActionResult<ProductReadDto>> CreateProduct(ProductCreateDto dto)
    {
        logger.LogInformation("{ProductName} - nomli yangi mahsulor yaratilmoqda", dto.Name);
        var product = new Product
        {
            Name = dto.Name,
            Price = dto.Price,
            Status = dto.Status
        };

        var createdProduct = productService.CreateProduct(product);

        logger.LogInformation("{createdProduct.Id} - Id li yangi mahsulot yaratildi", createdProduct.Id);

        return CreatedAtAction(nameof(GetProductById), new { id = createdProduct.Id }, new ProductReadDto
        {
            Id = createdProduct.Id,
            Name = createdProduct.Name,
            Price = createdProduct.Price,
            CreatedAt = createdProduct.CreatedAt,
            ModifiedAt = createdProduct.ModifiedAt,
            Status = createdProduct.Status
        }
        );
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<ProductReadDto>> GetProductById(Guid id)
    {
        logger.LogInformation("{id} - Id li mahsulot qidirilmoqda", id);
    
        var product = await productService.GetProductById(id);
   
        if (product == null)
        {
            logger.LogWarning("{id} - idli mahsulot topilmadi", id);
            return NotFound();

        }



        return Ok(new ProductReadDto
        {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price,
            CreatedAt = product.CreatedAt,
            ModifiedAt = product.ModifiedAt,
            Status = product.Status
        });
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductReadDto>>> GettAllProducts()
    {
        logger.LogInformation("Barcha mahsulotlar olinmoqda");
        
        var products = productService.GetAllProducts();
        
        logger.LogInformation("{products.Count} ta mahsulot topildi", products.Count());
        
        return Ok(products.Select(p => new ProductReadDto
        {
            Id = p.Id,
            Name = p.Name,
            Price = p.Price,
            CreatedAt = p.CreatedAt,
            ModifiedAt = p.ModifiedAt,
            Status = p.Status
        }));
    }


    [HttpPut("{id}")]
    public async Task<ActionResult<ProductReadDto>> UpdateProduct(Guid id, ProductUpdateDto dto)
    {
        logger.LogInformation("{id} - idli mahsulot yangilanmoqda", id);
        var product = new Product
        {
            Name = dto.Name,
            Price = dto.Price,
            Status = dto.Status
        };

        var updatedProduct = productService.UpdateProduct(id, product);
        if (updatedProduct == null)
        {
            logger.LogWarning("Yangilashni imkoni bo'lmadi. {id} - idli mahsulot topilmadi", id);
            return NotFound();

        }
        logger.LogInformation("{id} - idli mahsulot yangilandi.", id);
        return Ok(new ProductReadDto
        {
            Id = updatedProduct.Id,
            Name = updatedProduct.Name,
            Price = updatedProduct.Price,
            CreatedAt = updatedProduct.CreatedAt,
            ModifiedAt = updatedProduct.ModifiedAt,
            Status = updatedProduct.Status
        });
    }


    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteProduct(Guid id)
    {
        logger.LogInformation("{id} - idli mahsulot o'chrilmoqda", id);
        var deleted = productService.DeleteProduct(id);
        if (!deleted)
        {
            logger.LogError("O'chirish uchun {id} idli mahsulot topilmadi", id);
            return NotFound();

        }

        logger.LogInformation("{id} - idli mahsulot o'chrildi", id);
        return NoContent();
    }
}


