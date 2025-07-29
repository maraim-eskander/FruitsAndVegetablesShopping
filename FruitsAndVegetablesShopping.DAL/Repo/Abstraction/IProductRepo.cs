using FruitsAndVegetablesShopping.DAL.Entities;

namespace FruitsAndVegetablesShopping.DAL.Repo.Abstraction
{
    public interface IProductRepo
    {
        (List<Product>, string?) GetAll();
        (Product?, string?) GetById(int id);
        (bool, string?) Create(Product product);
        (bool, string?) Update(int id, string name, double price, string image, int stock, string desc, int categoryId, string modifiedBy);
        (bool, string?) Delete(int id, string deletedBy);
    }
}
