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
    public (bool, string?) Create(Orders order)
    {
        try
        {
            db.Orders.Add(order);
            db.SaveChanges();
            return (true, null);

        }catch(Exception ex)
        {
            return (false, ex.Message);
        }
    }

    public (bool, string) Edit(int id, float total, int cnt)
    {
        try
        {
            var res = db.Orders.Where(a => a.Order_id == id).FirstOrDefault();

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

    public (bool, string?) Delete(int id)
    {
        try
        {
            var res = db.Orders.Where(a => a.Order_id == id).FirstOrDefault();

            if(res == null)
            {
                return (false, "Does not exist in db");
            }

            res.delete();
            db.SaveChanges();
            return (true, null);
        }catch(Exception ex)
        {
            return (false, ex.Message);
        }
    }

    

    public (List<Orders>?, string?) GetAll()
    {
        try
        {
            var res = db.Orders.Where(a=>a.isDeleted == false).ToList();

            if(res == null)
            {
                return (null, "Db empty no list");
            }

            return (res, null);
        }catch(Exception ex)
        {
            return (null, ex.Message);
        }
    }

    public (Orders?, string?) GetById(int id)
    {
        try
        {
            var res = db.Orders.Where(a => a.Order_id == id).FirstOrDefault();

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
