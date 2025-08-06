using System.ComponentModel.DataAnnotations;

namespace Data.Entities.Product;

public class ProductProperty
{
    [Key]
    public int PP_ID { get; set; }

    public int ProductID { get; set; }
    public string Titel { get; set; }
    public string Value { get; set; }


    public Product Product { get; set; }
}