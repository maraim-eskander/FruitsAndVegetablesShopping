using FruitsAndVegetablesShopping.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace FruitsAndVegetablesShopping.DAL.Database
{
    public class ShoppingDbContext: DbContext
    {


        public ShoppingDbContext(DbContextOptions<ShoppingDbContext> options) : base(options)
        {
        }
        public DbSet<Category> Categories{ get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Delivery> Deliveries { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Admin> Admins { get; set; }

    }
}
