using FruitsAndVegetablesShopping.DAL.Entities;

namespace FruitsAndVegetablesShopping.DAL.Repo.Abstraction
{


public interface IProductCartRepo
{
    (List<ProductCart>?, string?) GetAll();
    (ProductCart?, string?) GetById(int id);
    (bool, string?) Create(ProductCart productOrder);
    (bool, string?) Update(int id, int qty);
    (bool, string?) Delete(int id, string deletedBy);
}



}
