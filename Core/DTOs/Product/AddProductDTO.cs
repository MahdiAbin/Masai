using Microsoft.AspNetCore.Http;

namespace Core.DTOs.Product;

public class AddProductDTO
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Brand { get; set; }
    public int Quantity { get; set; }
    public int Discount { get; set; }
    public int Price { get; set; }
    public IFormFile? ImageName { get; set; }

}