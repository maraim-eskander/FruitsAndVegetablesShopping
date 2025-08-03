using FruitsAndVegetablesShopping.BLL.ModelVm.Product;
namespace FruitsAndVegetablesShopping.BLL.Services.Abstraction
{
    public interface IProductService
    {

        Task<(bool, string?)> CreateAsync(CreateProductDto dto);
        Task<(bool, string?)> UpdateAsync(int id, UpdateProductDto dto);
        Task<(bool, string?)> DeleteAsync(int id, string deletedBy);
        Task<(ReadProductDto?, string?)> GetByIdAsync(int id);
        Task<(List<ReadProductDto>, string?)> GetAllAsync();
        Task<(List<ReadProductDto>, string?)> SearchByNameAsync(string name);

    }
}
