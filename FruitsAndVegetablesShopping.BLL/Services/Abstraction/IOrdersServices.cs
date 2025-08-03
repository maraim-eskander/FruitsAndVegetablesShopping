using FruitsAndVegetablesShopping.BLL.ModelVm.Product;
namespace FruitsAndVegetablesShopping.BLL.Services.Abstraction
{
public interface IOrdersServices
{
    public Task<(bool, string)> CreateAsync(OrdersDTO ordersDTO);

    public Task<(bool, string?)> EditAsync(OrdersDTO orderDTO);

    public Task<(Orders?, string?)> GetByIdAsync(int id);

    public Task<(List<Orders>?, string?)> GetAllAsync();

    public Task<(bool, string?)> DeleteAsync(int id);
}
}
