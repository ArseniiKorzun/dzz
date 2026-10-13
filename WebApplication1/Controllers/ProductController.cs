using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class ProductController : Controller
    {
        private static readonly List<Product> _products = new()
        {
            new Product { Id = 1, Name = "Ноутбук", Price = 25000 },
            new Product { Id = 2, Name = "Смартфон", Price = 15000 },
            new Product { Id = 3, Name = "Навушники", Price = 2000 }
        };
        private static readonly object _lock = new();

        public IActionResult Index()
        {
            lock (_lock)
                return View(_products.ToList());
        }

        // /Product/Details/5
        public IActionResult Details(int id)
        {
            Product? product;
            lock (_lock)
                product = _products.FirstOrDefault(p => p.Id == id);
            return product == null ? NotFound() : View(product);
        }

        // /Product/Search?name=ноут
        public IActionResult Search(string? name)
        {
            ViewData["Query"] = name;
            List<Product> result;
            lock (_lock)
                result = string.IsNullOrWhiteSpace(name)
                    ? new List<Product>()
                    : _products.Where(p => p.Name.Contains(name.Trim(), StringComparison.OrdinalIgnoreCase)).ToList();
            return View(result);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new Product());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Name,Price")] Product product)
        {
            if (!ModelState.IsValid)
                return View(product);

            lock (_lock)
            {
                product.Id = _products.Count == 0 ? 1 : _products.Max(p => p.Id) + 1;
                _products.Add(product);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
