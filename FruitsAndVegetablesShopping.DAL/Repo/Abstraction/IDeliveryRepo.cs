using FruitsAndVegetablesShopping.DAL.Entities;


namespace FruitsAndVegetablesShopping.DAL.Repo.Abstraction
{
    public interface IDeliveryRepo
    {
        (List<Delivery>, string?) GetAll();
        (Delivery?, string?) GetById(int id);
        (bool, string?) Create(Delivery delivery);
        (bool, string?) Update(int id, string name, string modifiedBy);
        (bool, string?) Delete(int id, string deletedBy);
    }
}
