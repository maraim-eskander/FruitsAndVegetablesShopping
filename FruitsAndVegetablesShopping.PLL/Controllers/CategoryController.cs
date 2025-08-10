using FruitsAndVegetablesShopping.BLL.ModelVm.Category;
using FruitsAndVegetablesShopping.BLL.Services.Abstraction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FruitsAndVegetablesShopping.PL.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

  
        // ADMIN
     
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(CreateCategoryDto dto)
        {
            dto.CreatedBy = User.Identity?.Name ?? "Admin";

            var (success, error) = await categoryService.CreateAsync(dto);

            if (success)
            {
                TempData["SuccessMessage"] = "Category created successfully!";
                return RedirectToAction(nameof(GetAll));
            }

            ViewBag.ErrorMessage = error;
            return View(dto);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id)
        {
            var (category, error) = await categoryService.GetByIdAsync(id);
            if (category == null)
            {
                TempData["ErrorMessage"] = error;
                return RedirectToAction(nameof(GetAll));
            }

            var updateDto = new UpdateCategoryDto
            {
                Name = category.Name
            };

            return View(updateDto);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, UpdateCategoryDto dto)
        {
            dto.ModifiedBy = User.Identity?.Name ?? "Admin";

            var (success, error) = await categoryService.UpdateAsync(id, dto);

            if (success)
            {
                TempData["SuccessMessage"] = "Category updated successfully!";
                return RedirectToAction(nameof(GetAll));
            }

            ViewBag.ErrorMessage = error;
            return View(dto);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var (category, error) = await categoryService.GetByIdAsync(id);
            if (category == null)
            {
                TempData["ErrorMessage"] = error;
                return RedirectToAction(nameof(GetAll));
            }

            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            string deletedBy = User.Identity?.Name ?? "Admin";

            var (success, error) = await categoryService.DeleteAsync(id, deletedBy);

            if (success)
                TempData["SuccessMessage"] = "Category deleted successfully!";
            else
                TempData["ErrorMessage"] = error;

            return RedirectToAction(nameof(GetAll));
        }


        // USER

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            var (categories, error) = await categoryService.GetAllAsync();

            if (categories == null)
            {
                ViewBag.ErrorMessage = error;
                return View(new List<ReadCategoryDto>());
            }

            return View(categories);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var (category, error) = await categoryService.GetByIdAsync(id);
            if (category == null)
            {
                TempData["ErrorMessage"] = error;
                return RedirectToAction(nameof(GetAll));
            }

            return View(category);
        }
    }
}
