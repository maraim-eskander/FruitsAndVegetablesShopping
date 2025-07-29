using FruitsAndVegetablesShopping.DAL.Entities;

namespace FruitsAndVegetablesShopping.DAL.Repo.Abstraction
{
    public interface ICategoryRepo
    {
        (List<Category>, string?) GetAll();
        (Category?, string?) GetById(int id);
        (bool, string?) Create(Category category);
        (bool, string?) Update(int id, string name, string modifiedBy);
        (bool, string?) Delete(int id, string deletedBy);
    }
}
