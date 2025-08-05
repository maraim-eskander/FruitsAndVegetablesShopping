using Microsoft.EntityFrameworkCore;
using MVC_Group7_demo_DAL.DataBase;
using MVC_Group7_demo_DAL.Entities;
using MVC_Group7_demo_DAL.Repository.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Group7_demo_DAL.Repository.Implementation
{
    public class CustomerRepo : ICustomerRepo
    {

        private readonly Context db;
        public CustomerRepo(Context db)
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

        public (bool, string?) Delete(string id, string deletedBy)
        {
            try
            {
                var customer = db.Customers.Where(c => c.UserId == id).FirstOrDefault();
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

        public (Customer?, string?) GetById(string id)
        {
            try
            {
                var res = db.Customers.Include(c=>c.Orders).Where(c => c.UserId == id).FirstOrDefault();

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

        public (bool, string?) Update(string id, string name, string modifiedBy)
        {
            var existing = db.Customers.Where(c => c.UserId == id).FirstOrDefault();
            if (existing == null)
                return (false, "Customer not found");

            existing.UpdateName(name, modifiedBy);
            db.SaveChanges();
            return (true, null);
        }
    }
}
