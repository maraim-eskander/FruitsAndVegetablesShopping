using Microsoft.AspNetCore.Mvc;

namespace FruitsAndVegetablesShopping.PLL.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
