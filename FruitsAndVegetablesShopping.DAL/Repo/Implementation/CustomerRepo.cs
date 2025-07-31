using FruitsAndVegetablesShopping.DAL.Database;
using FruitsAndVegetablesShopping.DAL.Entities;
using FruitsAndVegetablesShopping.DAL.Repo.Abstraction;

namespace FruitsAndVegetablesShopping.DAL.Repo.Implementation
{


    public class CustomerRepo : ICustomerRepo
    {

        private readonly ShoppingDbContext db;
        public CustomerRepo(ShoppingDbContext db)
        {
            this.db = db;
        }

        public (bool, string?) Create(Customer customer)
        {

            try
            {
                db.Customers.Add(customer);
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
                var customer = db.Customers.Where(c => c.CustomerId == id).FirstOrDefault();
                if (customer == null)
                    return (false, "Customer not found");

                customer.Delete(id, deletedBy);
                db.SaveChanges();
                return (true, null);
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        public (List<Customer>?, string?) GetAll()
        {
            try
            {
                var res = db.Customers.Where(c => c.IsDeleted == false).ToList();
                return (res, null);
            }
            catch (Exception ex)
            {
                return (new List<Customer>(), ex.Message);
            }
        }

        public (Customer?, string?) GetById(int id)
        {
            try
            {
                var res = db.Customers.Where(c => c.CustomerId == id).FirstOrDefault();

                if (res == null)
                {
                    return (null, "Customer not found");
                }

                return (res, null);
            }
            catch (Exception ex)
            {
                return (null, ex.Message);
            }
            ;
        }

        public (bool, string?) Update(int id, string name, string modifiedBy)
        {
            var existing = db.Customers.Where(c => c.CustomerId == id).FirstOrDefault();
            if (existing == null)
                return (false, "Customer not found");

            existing.UpdateName(name, modifiedBy);
            db.SaveChanges();
            return (true, null);
        }
    }
}
