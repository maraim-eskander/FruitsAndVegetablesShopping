
using FruitsAndVegetablesShopping.DAL.Database;
using FruitsAndVegetablesShopping.DAL.Entities;
using FruitsAndVegetablesShopping.DAL.Repo.Abstraction;

namespace FruitsAndVegetablesShopping.DAL.Repo.Implementation
{
    public class AdminRepo : IAdminRepo
    {
        private readonly ShoppingDbContext db;
        public AdminRepo(ShoppingDbContext db)
        {
            this.db = db;
        }

        public (bool, string?) CreateAdmin(string adminName, string createdBy)
        {
            try
            {
                // Updated to use a constructor that matches the required parameters
                var admin = new Admin(
                    adminId: 0, // Assuming 0 for new Admins, as AdminId is likely auto-generated
                    name: adminName,
                    phone: string.Empty, // Default value, update as needed
                    email: string.Empty, // Default value, update as needed
                    password: string.Empty, // Default value, update as needed
                    createdBy: createdBy
                );

                db.Admins.Add(admin);
                db.SaveChanges();
                return (true, null);
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        public (bool, string?) DeleteAdmin(int id, string deletedBy)
        {
            try
            {
                var admin = db.Admins.Where(c => c.AdminId == id).FirstOrDefault();
                if (admin == null)
                    return (false, "Admin not found");

                admin.Delete(id, deletedBy);
                db.SaveChanges();
                return (true, null);
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        public (Admin?, string?) GetAdminById(int id)
        {
            try
            {
                var res = db.Admins.Where(c => c.AdminId == id).FirstOrDefault();

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

        public (List<Admin>?, string?) GetAllAdmins()
        {
            try
            {
                var res = db.Admins.Where(c => c.IsDeleted == false).ToList();
                return (res, null);
            }
            catch (Exception ex)
            {
                return (new List<Admin>(), ex.Message);
            }
        }

        public (bool, string?) UpdateAdmin(int id, string adminName, string modifiedBy)
        {
            var existing = db.Admins.Where(c => c.AdminId == id).FirstOrDefault();
            if (existing == null)
                return (false, "Admin not found");

            existing.UpdateName(adminName, modifiedBy);
            db.SaveChanges();
            return (true, null);
        }

        (Admin?, string?) IAdminRepo.GetAdminById(int id)
        {
            try
            {
                var res = db.Admins.Where(c => c.AdminId == id).FirstOrDefault();

                if (res == null)
                {
                    return (null, "Admin not found");
                }

                return (res, null);
            }
            catch (Exception ex)
            {
                return (null, ex.Message);
            }
            ;
        }
    }
}
