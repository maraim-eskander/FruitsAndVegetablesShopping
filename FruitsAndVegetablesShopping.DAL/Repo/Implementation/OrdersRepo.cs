
using FruitsAndVegetablesShopping.DAL.Database;
using FruitsAndVegetablesShopping.DAL.Entities;
using FruitsAndVegetablesShopping.DAL.Repo.Abstraction;
namespace FruitsAndVegetablesShopping.DAL.Repo.Implementation
{

public class OrdersRepo : IOrdersRepo
{
    private readonly Context db;

    public OrdersRepo(Context db)
    {
        this.db = db;
    }

    public async Task<(bool, string?)> CreateAsync(Orders order)
    {
        try
        {
            await db.Orders.AddAsync(order);
            db.SaveChanges();
            return (true, null);
        }
        catch (Exception ex)
        {
            return (false, ex.Message);
        }
    }

    public async Task<(bool, string?)> EditAsync(int id, float total, int cnt)
    {
        try
        {
            var res = await db.Orders.FirstOrDefaultAsync(a => a.Order_id == id);

            if (res == null)
            {
                return (false, "Does not exist in db");
            }

            res.edit(total, cnt);
            db.SaveChanges();
            return (true, null);
        }
        catch (Exception ex)
        {
            return (false, ex.Message);
        }
    }

    public async Task<(bool, string?)> DeleteAsync(int id)
    {
        try
        {
            var res = await db.Orders
                .Include(o => o.customer)
                .Include(o => o.ProductOrders) 
                .FirstOrDefaultAsync(a => a.Order_id == id);

            if (res == null)
            {
                return (false, "Does not exist in db");
            }

            res.delete(res.customer.Name);
            db.SaveChanges();
            return (true, null);
        }
        catch (Exception ex)
        {
            return (false, ex.Message);
        }
    }

    public async Task<(List<Orders>?, string?)> GetAllAsync()
    {
        try
        {
            var res = await db.Orders.Where(a => !a.isDeleted).ToListAsync();

            if (res == null || res.Count == 0)
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

    public async Task<(Orders?, string?)> GetByIdAsync(int id)
    {
        try
        {
            var res = await db.Orders.FirstOrDefaultAsync(a => a.Order_id == id);

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

