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
        private readonly AppDbContext _context;

        // Впроваджуємо репозиторій для товарів та контекст для швидкого завантаження категорій
        public CatalogController(IProductRepository productRepository, AppDbContext context)
        {
            _productRepository = productRepository;
            _context = context;
        }

        // 1. ГОЛОВНА СТОРІНКА КАТАЛОГУ (З підтримкою фільтрації за категоріями)
        // GET: /Catalog або /Catalog?category=Електроніка
        public async Task<IActionResult> Index(string? category)
        {
            IEnumerable<Product> products;

            // Фільтруємо товари, якщо категорію передано в URL і це не варіант "Всі"
            if (!string.IsNullOrEmpty(category) && category != "Всі")
            {
                products = await _productRepository.GetProductsByCategoryNameAsync(category);
                ViewBag.SelectedCategory = category; // Запам'ятовуємо активну категорію для підсвічування кнопки
            }
            else
            {
                products = await _productRepository.GetAllProductsAsync();
                ViewBag.SelectedCategory = "Всі";
            }

            // Отримуємо з бази даних список лише НАЗВ усіх категорій для створення кнопок-фільтрів
            ViewBag.CategoriesList = await _context.Categories.Select(c => c.Name).ToListAsync();

            return View(products);
        }

        // 2. СТВОРЕННЯ ТОВАРУ (Відображення форми GET)
        public async Task<IActionResult> Create()
        {
            // Формуємо випадаючий список категорій з існуючих у БД
            ViewBag.Categories = new SelectList(await _context.Categories.ToListAsync(), "Id", "Name");
            return View();
        }

        // 3. СТВОРЕННЯ ТОВАРУ (Збереження в БД POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product)
        {
            // Перевіряємо валідацію моделі (атрибути [Required], [Range] тощо)
            if (ModelState.IsValid)
            {
                await _productRepository.CreateProductAsync(product);
                return RedirectToAction(nameof(Index));
            }

            // Якщо дані некоректні, перестворюємо список категорій та повертаємо користувача на форму з помилками
            ViewBag.Categories = new SelectList(await _context.Categories.ToListAsync(), "Id", "Name", product.CategoryId);
            return View(product);
        }

        // 4. РЕДАГУВАННЯ ТОВАРУ (Відображення форми GET)
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var product = await _productRepository.GetProductByIdAsync(id.Value);
            if (product == null) return NotFound();

            // Передаємо список категорій із попередньо обраною категорією цього товару
            ViewBag.Categories = new SelectList(await _context.Categories.ToListAsync(), "Id", "Name", product.CategoryId);
            return View(product);
        }

        // 5. РЕДАГУВАННЯ ТОВАРУ (Оновлення даних у БД POST)
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

        // 6. ВИДАЛЕННЯ ТОВАРУ (POST-запит безпечного видалення)
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _productRepository.DeleteProductAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
