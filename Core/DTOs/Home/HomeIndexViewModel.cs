using Data.Entities.Cart;
using Data.Entities.Category;
using Data.Entities.Product;

namespace Core.DTOs.Home_;

public class HomeIndexViewModel
{
    public List<Product> Products {get; set;}
    public List<Category> Parent { get; set; }
    public List<Cart> Carts { get; set; }
}