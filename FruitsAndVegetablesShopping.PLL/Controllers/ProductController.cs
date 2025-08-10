using FruitsAndVegetablesShopping.BLL.Helper;
using FruitsAndVegetablesShopping.BLL.ModelVm.Product;
using FruitsAndVegetablesShopping.BLL.Services.Abstraction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FruitsAndVegetablesShopping.PL.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService productService;

        public ProductController(IProductService productService)
        {
            this.productService = productService;
        }

        //  ADMIN CRUD 

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(CreateProductDto dto)
        {
            if (dto.File != null && dto.File.Length > 0)
            {
                var fileName = Upload.UploadFile("Files", dto.File);
                dto.Image = "/Files/" + fileName;
            }

            string createdBy;
            if (User.Identity != null && User.Identity.Name != null)
                createdBy = User.Identity.Name;
            else
                createdBy = "Admin";

            dto.CreatedBy = createdBy;

            var result = await productService.CreateAsync(dto);

            if (result.Item1)
            {
                TempData["SuccessMessage"] = "Product created successfully!";
                return RedirectToAction(nameof(GetAll));
            }

            ViewBag.ErrorMessage = result.Item2;
            return View(dto);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id)
        {
            var result = await productService.GetByIdAsync(id);
            if (result.Item1 == null)
            {
                TempData["ErrorMessage"] = result.Item2;
                return RedirectToAction(nameof(GetAll));
            }

            var updateDto = new UpdateProductDto
            {
                ProductId = id,
                Name = result.Item1.Name,
                Price = result.Item1.Price,
                Description = result.Item1.Description,
                Image = result.Item1.Image,
                Stock = result.Item1.Stock
            };

            return View(updateDto);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, UpdateProductDto dto)
        {
            if (dto.File != null && dto.File.Length > 0)
            {
                var fileName = Upload.UploadFile("Files", dto.File);
                dto.Image = "/Files/" + fileName;
            }

            string modifiedBy;
            if (User.Identity != null && User.Identity.Name != null)
                modifiedBy = User.Identity.Name;
            else
                modifiedBy = "Admin";

            dto.ModifiedBy = modifiedBy;

            var result = await productService.UpdateAsync(id, dto);

            if (result.Item1)
            {
                TempData["SuccessMessage"] = "Product updated successfully!";
                return RedirectToAction(nameof(GetAll));
            }

            ViewBag.ErrorMessage = result.Item2;
            return View(dto);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await productService.GetByIdAsync(id);
            if (result.Item1 == null)
            {
                TempData["ErrorMessage"] = result.Item2;
                return RedirectToAction(nameof(GetAll));
            }

            return View(result.Item1);
        }

        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            string displayName;
            if (User.Identity != null && User.Identity.Name != null)
                displayName = User.Identity.Name;
            else
                displayName = "Admin";

            var result = await productService.DeleteAsync(id, displayName);

            if (result.Item1)
                TempData["SuccessMessage"] = "Product deleted successfully!";
            else
                TempData["ErrorMessage"] = result.Item2;

            return RedirectToAction(nameof(GetAll));
        }

        // ---------------- USER VIEWS ----------------

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            var result = await productService.GetAllAsync();

            if (result.Item1 == null)
            {
                ViewBag.ErrorMessage = result.Item2;
                return View(new List<ReadProductDto>());
            }

            return View(result.Item1);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var result = await productService.GetByIdAsync(id);
            if (result.Item1 == null)
            {
                TempData["ErrorMessage"] = result.Item2;
                return RedirectToAction(nameof(GetAll));
            }

            return View(result.Item1);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ByCategory(int categoryId)
        {
            var result = await productService.GetByCategoryIdAsync(categoryId);

            if (result.Item1 == null)
            {
                ViewBag.ErrorMessage = result.Item2;
                return View(new List<ReadProductDto>());
            }

            return View("GetAll", result.Item1);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Search(string name)
        {
            var result = await productService.SearchByNameAsync(name);

            if (result.Item1 == null)
            {
                ViewBag.ErrorMessage = result.Item2;
                return View(new List<ReadProductDto>());
            }

            return View("GetAll", result.Item1);
        }
    }
}
