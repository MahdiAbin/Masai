using System.ComponentModel.DataAnnotations;

namespace Data.Entities.Product;

public class Product
{
    [Key]
    public int ProductID { get; set; }

    public string Name { get; set; }
    public string Description { get; set; }
    public string Brand { get; set; }
    public int Quantity { get; set; }
    public int Discount { get; set; }
    public int Price { get; set; }
    public string? ImageName { get; set; }


    public List<ProductProperty> Properties { get; set; }
}