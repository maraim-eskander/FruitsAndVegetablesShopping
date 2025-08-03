
using FruitsAndVegetablesShopping.DAL.Database;
using FruitsAndVegetablesShopping.DAL.Entities;
using FruitsAndVegetablesShopping.DAL.Repo.Abstraction;
namespace FruitsAndVegetablesShopping.DAL.Repo.Implementation
{
public class ProductCartRepo : IProductCartRepo
{
    private readonly Context db;

    public ProductCartRepo(Context db)
    {
        this.db = db;
    }
    public (bool, string?) Create(ProductCart productCart)
    {
        try
        {
            db.productCarts.Add(productCart);
            db.SaveChanges();
            return (true, null);

        }
        catch (Exception ex)
        {
            return (false, ex.Message);
        }
    }

    public (bool, string?) Delete(int id, string deletedBy)
    {
        try
        {
            var res = db.productCarts.Where(po => po.ProductCartId == id).FirstOrDefault();

            if (res == null)
            {
                return (false, "Does not exist");
            }

            res.delete(deletedBy);
            db.SaveChanges();
            return (true, null);

        }
        catch (Exception ex)
        {
            return (false, ex.Message);
        }
    }

    public (List<ProductCart>?, string?) GetAll()
    {
        try
        {
            var res = db.productCarts.ToList();

            if (res == null || res.Count == 0)
            {
                return (null, "Empty OrderProducts Table");
            }

            return (res, null);
        }
        catch (Exception ex)
        {
            return (null, ex.Message);
        }
    }

    public (ProductCart?, string?) GetById(int id)
    {
        try
        {
            var res = db.productCarts.Where(po => po.ProductCartId == id).FirstOrDefault();
                
            if (res == null)
            {
                return (null, "Does not exist");
            }


            return (res, null);

        }
        catch (Exception ex)
        {
            return (null, ex.Message);
        }
    }

    /*
    public (bool, string?) Update(int id, int qty)
    {
        try
        {
            var res = db.productCarts.Where(po => po.ProductCartId == id).FirstOrDefault();

            if (res == null)
            {
                return (false, "Does not exist");
            }

            res.edit(qty);

            return (true, null);

        }
        catch (Exception ex)
        {
            return (false, ex.Message);
        }
    }
    */
}


}

