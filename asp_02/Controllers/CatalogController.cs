using asp_02.Data;
using asp_02.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace asp_02.Controllers
{
    public class CatalogController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly AppDbContext _context; // залишаємо виключно для швидкого виклику списку категорій у ViewBag

        // Впроваджуємо РЕПОЗИТОРІЙ замість чистого DbContext для товарів
        public CatalogController(IProductRepository productRepository, AppDbContext context)
        {
            _productRepository = productRepository;
            _context = context;
        }

        // 1. Отримати всі товари (Каталог)
        public async Task<IActionResult> Index()
        {
            var products = await _productRepository.GetAllProductsAsync();
            return View(products);
        }

        // 2. Створення товару (GET)
        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = new SelectList(await _context.Categories.ToListAsync(), "Id", "Name");
            return View();
        }

        // 3. Створення товару (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product)
        {
            if (ModelState.IsValid)
            {
                await _productRepository.CreateProductAsync(product);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Categories = new SelectList(await _context.Categories.ToListAsync(), "Id", "Name", product.CategoryId);
            return View(product);
        }

        // 4. Редагування товару (GET)
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var product = await _productRepository.GetProductByIdAsync(id.Value);
            if (product == null) return NotFound();

            ViewBag.Categories = new SelectList(await _context.Categories.ToListAsync(), "Id", "Name", product.CategoryId);
            return View(product);
        }

        // 5. Редагування товару (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Product product)
        {
            if (id != product.Id) return NotFound();

            if (ModelState.IsValid)
            {
                await _productRepository.UpdateProductAsync(product);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Categories = new SelectList(await _context.Categories.ToListAsync(), "Id", "Name", product.CategoryId);
            return View(product);
        }

        // 6. Видалення товару (POST)
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _productRepository.DeleteProductAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
