using asp_02.Models;

namespace asp_02.Data
{
    public interface IProductRepository
    {
        Task CreateProductAsync(Product product);
        Task UpdateProductAsync(Product product);
        Task DeleteProductAsync(int id);
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product?> GetProductByIdAsync(int id);
        Task<Product?> GetProductByNameAsync(string name);
        Task<IEnumerable<Product>> GetProductsByCategoryNameAsync(string categoryName);
        Task<IEnumerable<Product>> GetProductsByPriceRangeAsync(decimal minPrice, decimal maxPrice);
    }
}
