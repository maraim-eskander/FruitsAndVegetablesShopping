
using FruitsAndVegetablesShopping.DAL.Database;
using FruitsAndVegetablesShopping.DAL.Entities;
using FruitsAndVegetablesShopping.DAL.Repo.Abstraction;
using Microsoft.EntityFrameworkCore;

namespace FruitsAndVegetablesShopping.DAL.Repo.Implementation
{

    public class ProductOrderRepo : IProductOrderRepo
    {
        private readonly Context db;

        public ProductOrderRepo(Context db)
        {
            this.db = db;
        }
        public (bool, string?) Create(ProductOrder productOrder)
        {
            try
            {
                db.productOrders.Add(productOrder);
                db.SaveChanges();
                return (true, null);

            }catch(Exception ex)
            {
                return (false, ex.Message);
            }
        }

        public (bool, string?) Delete(int id, string deletedBy)
        {
            try
            {
                var res = db.productOrders.Where(po => po.ProductOrderId == id).FirstOrDefault();

                if(res == null)
                {
                    return (false, "Does not exist");
                }

                res.delete(deletedBy);
                db.SaveChanges();
                return (true, null);

            }catch(Exception ex)
            {
                return (false, ex.Message);
            }
        }

        public (List<ProductOrder>?, string?) GetAll()
        {
            try
            {
                var res = db.productOrders.ToList();

                if(res == null || res.Count == 0)
                {
                    return (null, "Empty OrderProducts Table");
                }

                return (res, null);
            }catch(Exception ex)
            {
                return (null, ex.Message);
            }
        }

        public (ProductOrder?, string?) GetById(int id)
        {
            try
            {
                var res = db.productOrders.Where(po => po.ProductOrderId == id).FirstOrDefault();

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

        public (bool, string?) Update(int id, int qty, string modifiedBy)
        {
            try
            {
                var res = db.productOrders.Where(po => po.ProductOrderId == id).FirstOrDefault();

                if (res == null)
                {
                    return (false, "Does not exist");
                }

                res.edit(qty, modifiedBy);

                return (true, null);

            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }
    }
}
