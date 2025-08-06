using Data.Context;
using Data.Entities.Product;
using Data.Repository;
using Google;
using Microsoft.EntityFrameworkCore;

namespace Core.Services;

public class ProductService:IProductService
{
    MasaiContxet _contxet;

    public ProductService(MasaiContxet contxet)
    {
        _contxet = contxet;
    }

    public async Task<List<Product>> GetAllProducts()
    {
        return await _contxet
            .Products
            .ToListAsync();
    }

    public async Task<Product> GetProductById(int id)
    {
        return await _contxet
            .Products
            .FindAsync(id);
    }

    public async Task<int> AddProduct(Product product)
    {
        _contxet.Products.Add(product);
        await _contxet.SaveChangesAsync();
        return product.ProductID;
    }
}