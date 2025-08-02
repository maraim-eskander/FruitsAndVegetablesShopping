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

    public Task<(bool, string?)> CreateAsync(Cart cart)
    {
        try
        {
            db.Carts.Add(cart);
            db.SaveChanges();
            return Task.FromResult((true, (string?)null));
        }
        catch (Exception ex)
        {
            return Task.FromResult<(bool, string?)>((false, ex.Message));
        }
    }

    public async Task<(bool, string?)> DeleteAsync(int id)
    {
        try
        {
            var res = await db.Carts.Where(a => a.Cart_id == id).FirstOrDefaultAsync();

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

    public async Task<(List<Cart>?, string?)> GetAllAsync()
    {
        try
        {
            var res = await db.Carts.Where(a => a.isDeleted == false).ToListAsync();

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

    public async Task<(Cart?, string?)> GetByIdAsync(int id)
    {
        try
        {
            var res = await db.Carts.Where(a => a.Cart_id == id).FirstOrDefaultAsync();

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
