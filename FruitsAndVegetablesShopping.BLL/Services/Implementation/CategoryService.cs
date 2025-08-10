using FruitsAndVegetablesShopping.BLL.ModelVm.Category;
using FruitsAndVegetablesShopping.BLL.Services.Abstraction;
using FruitsAndVegetablesShopping.DAL.Entities;
using FruitsAndVegetablesShopping.DAL.Repo.Abstraction;

namespace FruitsAndVegetablesShopping.BLL.Services.Implementation
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepo categoryRepo;
        private readonly IProductRepo productRepo; // You'll need to inject this

        public CategoryService(ICategoryRepo categoryRepo, IProductRepo productRepo)
        {
            this.categoryRepo = categoryRepo;
            this.productRepo = productRepo;
        }

        public async Task<(bool, string?)> CreateAsync(CreateCategoryDto dto)
        {
            try
            {
                var category = new Category(dto.Name, dto.CreatedBy);
                return await categoryRepo.CreateAsync(category);
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        public async Task<(List<ReadCategoryDto>, string?)> GetAllAsync()
        {
            var (categories, error) = await categoryRepo.GetAllAsync();
            if (categories == null)
                return (new List<ReadCategoryDto>(), error);

            var dtoList = categories.Select(c => new ReadCategoryDto
            {
                Id = c.CategoryId,
                Name = c.Name,
                Products = c.Products.Select(p => p.Name).ToList()
            }).ToList();

            return (dtoList, null);
        }

        public async Task<(ReadCategoryDto?, string?)> GetByIdAsync(int id)
        {
            var (category, error) = await categoryRepo.GetByIdAsync(id);
            if (category == null)
                return (null, error);

            var dto = new ReadCategoryDto
            {
                Id = category.CategoryId,
                Name = category.Name,
                Products = category.Products.Select(p => p.Name).ToList()
            };

            return (dto, null);
        }

        public async Task<(bool, string?)> UpdateAsync(int id, UpdateCategoryDto dto)
        {
            try
            {
                return await categoryRepo.UpdateAsync(id, dto.Name, dto.ModifiedBy);
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        public async Task<(bool, string?)> DeleteAsync(int id, string deletedBy)
        {
            try
            {
               
                var (generalCategory, createError) = await GetOrCreateGeneralCategory(deletedBy);
                if (generalCategory == null)
                    return (false, $"Failed to create General category: {createError}");

                
                var moveResult = await productRepo.MoveCategoryProductsAsync(id, generalCategory.CategoryId, deletedBy);
                if (!moveResult.success)
                    return (false, $"Failed to move products: {moveResult.error}");

             
                return await categoryRepo.DeleteAsync(id, deletedBy);
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        private async Task<(Category?, string?)> GetOrCreateGeneralCategory(string createdBy)
        {
            try
            {
               
                var (existingCategory, _) = await categoryRepo.GetByNameAsync("General");
                if (existingCategory != null)
                    return (existingCategory, null);

                var generalCategory = new Category("General", createdBy);
                var (success, error) = await categoryRepo.CreateAsync(generalCategory);

                if (!success)
                    return (null, error);

                return await categoryRepo.GetByNameAsync("General");
            }
            catch (Exception ex)
            {
                return (null, ex.Message);
            }
        }
    }
}