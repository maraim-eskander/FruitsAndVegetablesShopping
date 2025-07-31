using FruitsAndVegetablesShopping.DAL.Entities;
namespace FruitsAndVegetablesShopping.DAL.Repo.Abstraction
{
    public interface ICustomerRepo
    {
        (Customer?, string?) GetById(int id);
        (List<Customer>?, string?) GetAll();
        (bool, string?) Create(Customer customer);
        (bool, string?) Update(int id, string name, string modifiedBy);
        (bool, string?) Delete(int id ,string deletedBy);
    }
}
