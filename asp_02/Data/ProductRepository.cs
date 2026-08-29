using asp_02.Models;
using Microsoft.EntityFrameworkCore;

namespace asp_02.Data
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        // 1. Створити товар
        public async Task CreateProductAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        // 2. Оновити товар
        public async Task UpdateProductAsync(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }

        // 3. Видалити товар
        public async Task DeleteProductAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
        }

        // 4. Отримати всі товари
        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _context.Products.Include(p => p.Category).ToListAsync();
        }

        // 5. Отримати товар по id
        public async Task<Product?> GetProductByIdAsync(int id)
        {
            return await _context.Products.Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id);
        }

        // 6. Отримати товар по name
        public async Task<Product?> GetProductByNameAsync(string name)
        {
            return await _context.Products.Include(p => p.Category).FirstOrDefaultAsync(p => p.Name == name);
        }

        // 7. Отримати товари по імені категорії
        public async Task<IEnumerable<Product>> GetProductsByCategoryNameAsync(string categoryName)
        {
            return await _context.Products
                .Include(p => p.Category)
                .Where(p => p.Category != null && p.Category.Name.ToLower() == categoryName.ToLower())
                .ToListAsync();
        }

        // 8. Отримати товари у вказаному ціновому діапазоні
        public async Task<IEnumerable<Product>> GetProductsByPriceRangeAsync(decimal minPrice, decimal maxPrice)
        {
            return await _context.Products
                .Include(p => p.Category)
                .Where(p => p.Price >= minPrice && p.Price <= maxPrice)
                .ToListAsync();
        }
    }
}
