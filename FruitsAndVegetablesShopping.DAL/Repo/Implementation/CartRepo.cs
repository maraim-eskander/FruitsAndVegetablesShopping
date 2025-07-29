using FruitsAndVegetablesShopping.DAL.Database;
using FruitsAndVegetablesShopping.DAL.Entities;
using FruitsAndVegetablesShopping.DAL.Repo.Abstraction;
namespace FruitsAndVegetablesShopping.DAL.Repo.Implementation{
  
  public class CartRepo : ICartRepo
{
    private readonly Context db;

    public CartRepo(Context db)
    {
        this.db = db;
    }
    public (bool, string?) Create(Cart cart)
    {
        try
        {
            db.Carts.Add(cart);
            db.SaveChanges();
            return (true, null);

        }
        catch (Exception ex)
        {
            return (false, ex.Message);
        }
    }

    public (bool, string?) Delete(int id)
    {
        try
        {
            var res = db.Carts.Where(a => a.Cart_id == id).FirstOrDefault();

            if (res == null)
            {
                return (false, "Id does not exist in db");
            }

            res.delete();
            db.SaveChanges();
            return (true, null);
        }
        catch (Exception ex)
        {
            return (false, ex.Message);
        }
    }

    public (List<Cart>?, string?) GetAll()
    {
        try
        {
            var res = db.Carts.Where(a => a.isDeleted == false).ToList();

            if (res == null)
            {
                return (null, "Db empty no list");
            }

            return (res, null);
        }
        catch (Exception ex)
        {
            return (null, ex.Message);
        }
    }

    public (Cart?, string?) GetById(int id)
    {
        try
        {
            var res = db.Carts.Where(a => a.Cart_id == id).FirstOrDefault();

            if (res == null)
            {
                return (null, "Id does not exist in db");
            }


            return (res, null);
        }
        catch (Exception ex)
        {
            return (null, ex.Message);
        }
    }
}

}
