using System.Text.Json;
using asp_02.Data;
using asp_02.Models;
using Microsoft.AspNetCore.Mvc;

namespace asp_02.Controllers
{
    public class CartController : Controller
    {
        private const string CartSessionKey = "Cart";
        private readonly IProductRepository _productRepository;

        public CartController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IActionResult> Index()
        {
            var cart = GetCart();
            var items = new List<CartItemViewModel>();

            foreach (var item in cart)
            {
                var product = await _productRepository.GetProductByIdAsync(item.Key);

                if (product == null || product.Amount <= 0)
                    continue;

                // Якщо доступна кількість стала меншою за кількість у кошику,
                // автоматично обмежуємо кількість у кошику.
                var quantity = Math.Min(item.Value, product.Amount);
                if (quantity > 0)
                    items.Add(new CartItemViewModel { Product = product, Quantity = quantity });
            }

            SaveCart(items.ToDictionary(x => x.Product.Id, x => x.Quantity));
            return View(items);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(int id)
        {
            var product = await _productRepository.GetProductByIdAsync(id);
            if (product == null || product.Amount <= 0)
                return RedirectToAction("Index", "Catalog");

            var cart = GetCart();

            if (cart.TryGetValue(id, out var quantity))
            {
                if (quantity < product.Amount)
                    cart[id] = quantity + 1;
            }
            else
            {
                cart[id] = 1;
            }

            SaveCart(cart);
            return RedirectToAction("Index", "Catalog");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Increase(int id)
        {
            var product = await _productRepository.GetProductByIdAsync(id);
            var cart = GetCart();

            if (product != null && cart.TryGetValue(id, out var quantity))
            {
                if (quantity < product.Amount)
                    cart[id] = quantity + 1;
            }

            SaveCart(cart);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Decrease(int id)
        {
            var cart = GetCart();

            if (cart.TryGetValue(id, out var quantity))
            {
                quantity--;

                if (quantity <= 0)
                    cart.Remove(id);
                else
                    cart[id] = quantity;
            }

            SaveCart(cart);
            return RedirectToAction(nameof(Index));
        }

        private Dictionary<int, int> GetCart()
        {
            var json = HttpContext.Session.GetString(CartSessionKey);
            if (string.IsNullOrEmpty(json))
                return new Dictionary<int, int>();

            return JsonSerializer.Deserialize<Dictionary<int, int>>(json)
                   ?? new Dictionary<int, int>();
        }

        private void SaveCart(Dictionary<int, int> cart)
        {
            HttpContext.Session.SetString(CartSessionKey, JsonSerializer.Serialize(cart));
        }
    }
}
