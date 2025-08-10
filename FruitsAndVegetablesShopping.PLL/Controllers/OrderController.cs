using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVC_Group7_demo_BLL.ModelVM;
using MVC_Group7_demo_BLL.Services.Abstraction;
using MVC_Group7_demo_DAL.Entities;

namespace MVC_Group7_demo_PLL.Controllers
{
    public class OrderController : Controller
    {

        
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IOrdersServices ordersServices;

        public OrderController(UserManager<ApplicationUser> userManager, IOrdersServices ordersServices)
        {
            
            this.userManager = userManager;
            this.ordersServices = ordersServices;
        }

        public async Task<IActionResult> ChoosePayment()
        {
            return View();
        }

        public async Task<IActionResult> CardPayment()
        {
            return View();
        }

        public async Task<IActionResult> Delete(int id)
        {
            var res = await ordersServices.DeleteAsync(id);

            if (res.Item1)
            {
                return RedirectToAction("ViewOrder", "MainMenu");
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> EditOrder()
        {
            var user = await userManager.GetUserAsync(User);
            if (user == null)
            {
                TempData["Error"] = "You must be logged in to edit your order.";
                return RedirectToAction("Login", "Account");
            }

            var res = await ordersServices.GetCurrentOrder(user.Id);
            if (res.Item1 == null)
            {
                TempData["Error"] = "No active order found.";
                return RedirectToAction("Index", "Products");
            }

            List<ProductOrderEditDTO> productOrderDTOs = new List<ProductOrderEditDTO>();

            foreach (var po in res.Item1.ProductOrders)
            {
                productOrderDTOs.Add(new ProductOrderEditDTO()
                {
                    ProductOrderId = po.ProductOrderId,
                    ProductId = po.ProductId,
                    Quantity = po.Quantity,
                    ProductName = po.ProductName,
                    UnitPrice = po.UnitPrice,
                    ProductImage = po.ProductImage,
                    Stock = po.Stock,
                    Remove = false
                });
            }

            var editOrderDTO = new OrderEditDTO()
            {
                OrderId = res.Item1.OrderId,
                CustomerId = res.Item1.CustomerId,
                ProductOrders = productOrderDTOs
            };

            return View(editOrderDTO);
        }

        [HttpPost]
        public async Task<IActionResult> EditOrder(OrderEditDTO orderEditDTO)
        {

            /*
            if (!ModelState.IsValid)
            {
                
                return View(orderEditDTO);
            }
            */

            Console.WriteLine(orderEditDTO.OrderId);
            var user = await userManager.GetUserAsync(User);
            if (user == null)
            {
                TempData["Error"] = "You must be logged in to edit your order.";
                return RedirectToAction("Login", "Account");
            }
            Console.WriteLine("e7na hena");
            var res = await ordersServices.ApplyChanges(orderEditDTO, user.Id);
            if (res.Item1)
            {
                return RedirectToAction("ViewOrder", "MainMenu");
            }
            Console.WriteLine(res.Item2);
            Console.WriteLine("7asal ezay");

            ModelState.AddModelError("", res.Item2 ?? "Failed to apply changes.");
            return View(orderEditDTO);
        }
    }
}
