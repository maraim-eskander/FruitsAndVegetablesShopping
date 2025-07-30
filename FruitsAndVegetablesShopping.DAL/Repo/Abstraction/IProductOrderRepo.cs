using FruitsAndVegetablesShopping.DAL.Entities;

namespace FruitsAndVegetablesShopping.DAL.Repo.Abstraction
{

public interface IProductOrderRepo
{
    (List<ProductOrder>?, string?) GetAll();
    (ProductOrder?, string?) GetById(int id);
    (bool, string?) Create(ProductOrder productOrder);
    (bool, string?) Update(int id, int qty);
    (bool, string?) Delete(int id, string deletedBy);
}

}
