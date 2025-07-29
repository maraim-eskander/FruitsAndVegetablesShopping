using FruitsAndVegetablesShopping.DAL.Entities;

namespace FruitsAndVegetablesShopping.DAL.Repo.Abstraction{

  public interface IOrdersRepo
{
    (bool, string?) Create(Orders order);

    (bool, string) Edit(); 

    (Orders?, string?) GetById(int id);

    (List<Orders>?, string?) GetAll();

    (bool, string?) Delete(int id);
}


}
