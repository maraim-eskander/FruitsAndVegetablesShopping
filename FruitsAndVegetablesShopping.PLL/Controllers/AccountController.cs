
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVC_Group7_demo_BLL.ModelVM;
using MVC_Group7_demo_BLL.Services.Abstraction;
using MVC_Group7_demo_DAL.Entities;

namespace MVC_Group7_demo_PLL.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ICustomerServices customerServices;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, ICustomerServices customerServices, SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.customerServices = customerServices;
            this.signInManager = signInManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(RegisterVM registerVM)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", registerVM);
            }

            var user = new ApplicationUser
            {
                UserName = registerVM.Email,
                Email = registerVM.Email,
                PhoneNumber = registerVM.Phone,
                EmailConfirmed = true
            };

            var res = await userManager.CreateAsync(user, registerVM.Password);

            if (!res.Succeeded)
            {
                foreach (var error in res.Errors)
                    ModelState.AddModelError("", error.Description);
                return View("Index", registerVM);
            }

            await userManager.AddToRoleAsync(user, "Customer");

            var cusDTO = new CustomerDTO
            {
                UserId = user.Id,
                Name = registerVM.Name,
                Location = registerVM.Location
            };

            await customerServices.CreateAsync(cusDTO);


            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> LoginPage()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LogInVm model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                ModelState.AddModelError("", "Invalid login attempt.");
                return View("LoginPage", model);
            }

            var result = await signInManager.PasswordSignInAsync(
                user.UserName,  
                model.Password,
                model.RememberMe,
                lockoutOnFailure: false
            );

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "MainMenu");
            }

            ModelState.AddModelError("", "Invalid login attempt.");
            return View("LoginPage",model);
        }
    }
}
