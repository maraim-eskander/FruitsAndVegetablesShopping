using FruitsAndVegetablesShopping.DAL.Entities;

namespace FruitsAndVegetablesShopping.DAL.Repo.Abstraction
{
    public interface IProductRepo
    {
        (List<Product>, string?) GetAll();
        (Product?, string?) GetById(int id);
        (bool, string?) Create(Product product);
        (bool, string) Update(Product product);
        (bool, string) Delete(int id, string deletedBy);
    }
}
