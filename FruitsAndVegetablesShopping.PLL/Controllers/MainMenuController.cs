using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVC_Group7_demo_BLL.ModelVM;
using MVC_Group7_demo_BLL.Services.Abstraction;
using MVC_Group7_demo_BLL.Services.Implementation;
using MVC_Group7_demo_DAL.Entities;

namespace MVC_Group7_demo_PLL.Controllers
{
    public class MainMenuController : Controller
    {
        private readonly IProductService productService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IOrdersServices ordersServices;

        public MainMenuController(IProductService productService, UserManager<ApplicationUser> userManager, IOrdersServices ordersServices)
        {
            this.productService = productService;
            this.userManager = userManager;
            this.ordersServices = ordersServices;
        }

        public async Task<IActionResult> Index()
        {
            var (products, error) = await productService.GetAllAsync();

            if (!string.IsNullOrEmpty(error))
            {
                ViewBag.ErrorMessage = error;
                products = new List<ReadProductDto>();
            }

            return View(products);
        }

        public async Task<IActionResult> ShowProfile()
        {
            var user = await userManager.GetUserAsync(User);

            if (user == null)
            {
                return RedirectToAction("Login", "Account"); 
            }

            return View(user);  
        }

        [HttpPost]
        public async Task<IActionResult> AddToOrder(AddProductToOrderDTO dto)
        {
            var user = await userManager.GetUserAsync(User);

            if (user == null)
            {
                TempData["Error"] = "You must be logged in to add products to your order.";
                return RedirectToAction("Login", "Account");
            }

            //Console.WriteLine("In Controller : " + user.Id);

            var res = await ordersServices.AddProductOrderAsync(dto, user.Id);

            if (!res.Item1)
                TempData["Error"] = res.Item2;
            else
                TempData["Success"] = "Product added to order!";

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> ViewOrder()
        {
            var user = await userManager.GetUserAsync(User);

            if (user == null)
            {
                TempData["Error"] = "You must be logged in to add products to your order.";
                return RedirectToAction("Login", "Account");
            }

            var res = await ordersServices.GetCurrentOrder(user.Id);

            return View(res.Item1);
        }
    }
}
