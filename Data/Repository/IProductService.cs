using Data.Entities.Product;

namespace Data.Repository;

public interface IProductService
{
    public Task<List<Product>> GetAllProducts();
    public Task<Product> GetProductById(int id);
    public Task<int> AddProduct(Product product);
}