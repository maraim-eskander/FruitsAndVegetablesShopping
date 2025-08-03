using FruitsAndVegetablesShopping.DAL.Database;
using FruitsAndVegetablesShopping.DAL.Entities;
using FruitsAndVegetablesShopping.DAL.Repo.Abstraction;
using Microsoft.EntityFrameworkCore;

namespace FruitsAndVegetablesShopping.DAL.Repo.Implementation
{
    public class ProductRepo : IProductRepo
    {
        private readonly ShoppingDbContext db;

        public ProductRepo(ShoppingDbContext db)
        {
            this.db = db;
        }

        public async Task<(bool, string?)> CreateAsync(Product product)
        {
            try
            {
                await db.Products.AddAsync(product);
                await db.SaveChangesAsync();
                return (true, null);
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        public async Task<(bool, string?)> UpdateAsync(int id, string name, double price, string image, int stock, string desc, int categoryId, string modifiedBy)
        {
            try
            {
                var res = await db.Products.Where(p => p.ProductId == id && !p.IsDeleted).FirstOrDefaultAsync();

                if (res == null)
                {
                    return (false, "Product not found in database");
                }

                res.Update(name, price, image, stock, desc, categoryId, modifiedBy);
                await db.SaveChangesAsync();
                return (true, null);
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        public async Task<(bool, string?)> DeleteAsync(int id, string deletedBy)
        {
            try
            {
                var res = await db.Products.Where(p => p.ProductId == id).FirstOrDefaultAsync();

                if (res == null)
                {
                    return (false, "Product not found in database");
                }

                res.Delete(deletedBy);
                await db.SaveChangesAsync();
                return (true, null);
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        public async Task<(List<Product>?, string?)> GetAllAsync()
        {
            try
            {
                var res = await db.Products.Where(p => !p.IsDeleted).ToListAsync();

                if (res == null || res.Count == 0)
                {
                    return (null, "No products found in database");
                }

                return (res, null);
            }
            catch (Exception ex)
            {
                return (null, ex.Message);
            }
        }

        public async Task<(Product?, string?)> GetByIdAsync(int id)
        {
            try
            {
                var res = await db.Products.Where(p => p.ProductId == id).FirstOrDefaultAsync();

                if (res == null)
                {
                    return (null, "Product not found in database");
                }

                return (res, null);
            }
            catch (Exception ex)
            {
                return (null, ex.Message);
            }
        }
        public async Task<(List<Product>, string?)> GetByCategoryIdAsync(int categoryId)
        {
            try
            {
                var products = await db.Products.Where(p => p.CategoryId == categoryId && !p.IsDeleted).ToListAsync();

                if (products == null || products.Count == 0)
                    return (new List<Product>(), "No products found for this category");

                return (products, null);
            }
            catch (Exception ex)
            {
                return (new List<Product>(), ex.Message);
            }
        }
        public async Task<(List<Product>?, string?)> SearchByNameAsync(string name)
        {
            try
            {
                var res = await db.Products.Include(p => p.Category) .Where(p => !p.IsDeleted && p.Name.Contains(name)).ToListAsync();

                if (res == null || res.Count == 0)
                    return (null, "No products found matching the search");

                return (res, null);
            }
            catch (Exception ex)
            {
                return (null, ex.Message);
            }
        }


    }
}
