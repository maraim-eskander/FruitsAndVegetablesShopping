using FruitsAndVegetablesShopping.DAL.Database;
using FruitsAndVegetablesShopping.DAL.Entities;
using FruitsAndVegetablesShopping.DAL.Repo.Abstraction;
namespace FruitsAndVegetablesShopping.DAL.Repo.Implementation
{
    public class ProductRepo: IProductRepo
    {


        private readonly ShoppingDbContext db;


        public ProductRepo(ShoppingDbContext db)
        {
            this.db = db;
        }

        public (bool, string?) Create(Product product)
        {
            try
            {
                db.Products.Add(product);
                db.SaveChanges();
                return (true, null);
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }


        public (Product?, string?) GetById(int id)
        {
            try
            {
                var res = db.Products.Where(p => p.ProductId == id).FirstOrDefault();

                if (res == null)
                {
                    return (null, "Product not found");
                }

                return (res, null);
            }
            catch (Exception ex)
            {
                return (null, ex.Message);
            }
        }

        public (bool, string?) Update(int id, string name, double price, string image, int stock, string desc, int categoryId, string modifiedBy)
        {
            try
            {
                var existing = db.Products.FirstOrDefault(p => p.ProductId == id && p.IsDeleted == false);
                if (existing == null)
                    return (false, "Product not found");

                existing.Update(name, price, image, stock, desc, categoryId, modifiedBy);
                db.SaveChanges();
                return (true, null);
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        public (bool, string?) Delete(int id,string deletedBy)
        {
            try
            {
                var product = db.Products.FirstOrDefault(p => p.ProductId == id);
                if (product == null)
                    return (false, "Product not found");

                product.Delete(deletedBy);
                db.SaveChanges();
                return (true, null);
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        public (List<Product>, string?) GetAll()
        {
            try
            {
                var res = db.Products.Where(p => p.IsDeleted == false).ToList();
                return (res, null);
            }
            catch (Exception ex)
            {
                return (new List<Product>(), ex.Message);
            }
        }

    }
}