namespace Crud.Api.Dtos;

public class ProductUpdateDto
{
    public string? Name { get; set; }
    public decimal Price { get; set; }
    public string? Status { get; set; }
}