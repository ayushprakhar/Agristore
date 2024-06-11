using AgriShoppingCartMvcUI.Models;
using AgriShoppingCartMvcUI.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace AgriShoppingCartMvcUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHomeRepository _homeRepository;

        public HomeController(ILogger<HomeController> logger, IHomeRepository homeRepository)
        {
            _homeRepository = homeRepository;
            _logger = logger;
        }

        public async Task<IActionResult> Index(string sterm="",int CategoryId=0)
        {
            IEnumerable<Product> Products = await _homeRepository.GetProducts(sterm, CategoryId);
            IEnumerable<Category> Categories = await _homeRepository.Categories();
            ProductDisplayModel productModel = new ProductDisplayModel
            {
              Products=Products,
              Categories=Categories,
              STerm=sterm,
              CategoryId=CategoryId
            };
            return View(productModel);
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _homeRepository.GetProductById(id.Value);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Home()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}