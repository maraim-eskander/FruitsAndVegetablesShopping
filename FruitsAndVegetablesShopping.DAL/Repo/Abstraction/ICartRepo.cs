using FruitsAndVegetablesShopping.DAL.Entities;

namespace FruitsAndVegetablesShopping.DAL.Repo.Abstraction{
public interface ICartRepo
{
    (bool, string?) Create(Cart cart);

    //(bool, string) Edit(); //ynf3 aslan a3ml edit l Cart?

    (Cart?, string?) GetById(int id);

    (List<Cart>?, string?) GetAll();

    (bool, string?) Delete(int id);

    
}
}
