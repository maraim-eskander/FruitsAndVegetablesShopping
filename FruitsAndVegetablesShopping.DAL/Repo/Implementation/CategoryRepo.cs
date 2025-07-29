using FruitsAndVegetablesShopping.DAL.Entities;
using FruitsAndVegetablesShopping.DAL.Repo.Abstraction;
namespace FruitsAndVegetablesShopping.DAL.Repo.Implementation
{
    public class CategoryRepo: ICategoryRepo
    {





        
         private readonly DbContext db;

     
        public CategoryRepo(DbContext db)
        {
            this.db = db;
        }

        public (bool, string?) Create(Category category)
        {
            try
            {
                db.Categories.Add(category);
                db.SaveChanges();
                return (true, null);
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }


public (Category?, string?) GetById(int id)
{
    try
    {
        var res = db.Categories.Where(c => c.CategoryId == id ).FirstOrDefault();

        if (res == null)
        {
            return (null, "Category not found");
        }

        return (res, null);
    }
    catch (Exception ex)
    {
        return (null, ex.Message);
    }
}


       
        public (bool, string?) Update(int id, string name, string modifiedBy)
{
    var existing = db.Categories.Where(c => c.CategoryId == id).FirstOrDefault();
    if (existing == null)
        return (false, "Category not found");

    existing.UpdateName(name, modifiedBy);
    db.SaveChanges();
    return (true, null);
}

      
        public (bool, string?) Delete(int id, string deletedBy)
        {
            try
            {
                var category = db.Categories.Where(c => c.CategoryId == id).FirstOrDefault();
                if (category == null)
                    return (false, "Category not found");

                category.Delete(deletedBy);
                db.SaveChanges();
                return (true, null);
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        public (List<Category>, string?) GetAll()
      {   
        try
    {
        var res = db.Categories.Where(c => c.IsDeleted == false).ToList();
        return (res, null); 
    }
    catch (Exception ex)
    {
        return (new List<Category>(), ex.Message); 
    }
}



    }
}

     