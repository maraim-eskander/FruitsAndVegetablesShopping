using FruitsAndVegetablesShopping.DAL.Entities;

namespace FruitsAndVegetablesShopping.DAL.Repo.Abstraction
{
    public interface ICategoryRepo
    {
        Task<(bool, string?)> CreateAsync(Category category);
        Task<(bool, string?)> UpdateAsync(int id, string name, string modifiedBy);
        Task<(bool, string?)> DeleteAsync(int id, string deletedBy);
        Task<(List<Category>?, string?)> GetAllAsync();
        Task<(Category?, string?)> GetByIdAsync(int id);
    }
}
