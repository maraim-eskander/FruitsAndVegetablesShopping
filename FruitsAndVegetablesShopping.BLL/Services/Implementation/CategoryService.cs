using FruitsAndVegetablesShopping.BLL.ModelVm.Category;
using FruitsAndVegetablesShopping.BLL.Services.Abstraction;
using FruitsAndVegetablesShopping.DAL.Entities;
using FruitsAndVegetablesShopping.DAL.Repo.Abstraction;

namespace FruitsAndVegetablesShopping.BLL.Services.Implementation
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepo categoryRepo;

        public CategoryService(ICategoryRepo categoryRepo)
        {
            this.categoryRepo = categoryRepo;
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
                return await categoryRepo.DeleteAsync(id, deletedBy);
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }
    }
}
