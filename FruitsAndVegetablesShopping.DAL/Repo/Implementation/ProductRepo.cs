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

        public async Task<(bool, string?)> UpdateAsync(int id, string name, double price, string image, int stock, string desc, string modifiedBy)
        {
            try
            {
                var res = await db.Products.Where(p => p.ProductId == id && !p.IsDeleted).FirstOrDefaultAsync();

                if (res == null)
                {
                    return (false, "Product not found in database");
                }

                res.Update(name, price, image, stock, desc,  modifiedBy);
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
                var res = await db.Products.Include(p => p.Category).Where(p => !p.IsDeleted).ToListAsync();

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
                var res = await db.Products.Include(p => p.Category).Where(p => p.ProductId == id).FirstOrDefaultAsync();

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
                var products = await db.Products.Include(p => p.Category).Where(p => p.CategoryId == categoryId && !p.IsDeleted).ToListAsync();

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
                if (string.IsNullOrWhiteSpace(name))
                    return (null, "Search term is empty");

                var keyword = name.Trim().ToLower();

                var res = await db.Products.Include(p => p.Category).Where(p => !p.IsDeleted && p.Name.ToLower().Contains(keyword)) .ToListAsync();

                if (res.Count == 0)
                    return (null, "No products found matching the search");

                return (res, null);
            }
            catch (Exception ex)
            {
                return (null, ex.Message);
            }
        }

        public async Task<(bool, string?)> UpdateStockAsync(int productId, int newStock, string modifiedBy)
        {
            try
            {
                var product = await db.Products.Where(p => p.ProductId == productId && !p.IsDeleted).FirstOrDefaultAsync();

                if (product == null)
                    return (false, "Product not found");

                product.UpdateStock(newStock, modifiedBy);

                await db.SaveChangesAsync();
                return (true, null);
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        public async Task<(bool, string?)> CheckStockAsync(int productId, int quantity)
        {
            try
            {
                var product = await db.Products.Where(p => p.ProductId == productId && !p.IsDeleted).FirstOrDefaultAsync();

                if (product == null)
                    return (false, "Product not found");

                if (product.Stock < quantity)
                    return (false, $"Only {product.Stock} units available");

                return (true, null);
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }
        public async Task<(bool success, string? error)> MoveCategoryProductsAsync(int fromCategoryId, int toCategoryId, string modifiedBy)
        {
            try
            {
               
                var productsToMove = await db.Products.Where(p => p.CategoryId == fromCategoryId && !p.IsDeleted).ToListAsync();

               
                foreach (var product in productsToMove)
                {
                    product.UpdateCategory(toCategoryId, modifiedBy);
                }

                await db.SaveChangesAsync();
                return (true, null);
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }




    }
}
