using FruitsAndVegetablesShopping.DAL.Entities;

namespace FruitsAndVegetablesShopping.DAL.Repo.Abstraction
{
    public interface IProductRepo
    {
        Task<(bool, string?)> CreateAsync(Product product);
        Task<(bool, string?)> UpdateAsync(int id, string name, double price, string image, int stock, string desc,  string modifiedBy);
        Task<(bool, string?)> DeleteAsync(int id, string deletedBy);
        Task<(List<Product>?, string?)> GetAllAsync();
        Task<(Product?, string?)> GetByIdAsync(int id);
        Task<(List<Product>, string?)> GetByCategoryIdAsync(int categoryId);
        Task<(List<Product>?, string?)> SearchByNameAsync(string name);
        Task<(bool, string?)> UpdateStockAsync(int productId, int newStock, string modifiedBy);
        Task<(bool, string?)> CheckStockAsync(int productId, int quantity);

        Task<(bool success, string? error)> MoveCategoryProductsAsync(int fromCategoryId, int toCategoryId, string modifiedBy);

    }
}
