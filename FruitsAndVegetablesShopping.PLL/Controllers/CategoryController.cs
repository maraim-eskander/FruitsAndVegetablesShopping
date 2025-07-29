using Microsoft.AspNetCore.Mvc;

namespace FruitsAndVegetablesShopping.PLL.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
