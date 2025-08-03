using FruitsAndVegetablesShopping.BLL.ModelVm.Category;
namespace FruitsAndVegetablesShopping.BLL.Services.Abstraction
{
    public interface ICategoryService
    {
        Task<(bool, string?)> CreateAsync(CreateCategoryDto dto);
        Task<(bool, string?)> UpdateAsync(int id, UpdateCategoryDto dto);
        Task<(bool, string?)> DeleteAsync(int id, string deletedBy);
        Task<(ReadCategoryDto?, string?)> GetByIdAsync(int id);
        Task<(List<ReadCategoryDto>, string?)> GetAllAsync();


    }
}
