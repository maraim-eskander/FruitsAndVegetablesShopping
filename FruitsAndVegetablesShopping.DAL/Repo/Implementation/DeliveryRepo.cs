using FruitsAndVegetablesShopping.DAL.Database;
using FruitsAndVegetablesShopping.DAL.Entities;
using FruitsAndVegetablesShopping.DAL.Repo.Abstraction;


namespace FruitsAndVegetablesShopping.DAL.Repo.Implementation
{
    public class DeliveryRepo : IDeliveryRepo
    {
        private readonly ShoppingDbContext db;
        public DeliveryRepo(ShoppingDbContext db)
        {
            this.db = db;
        }

        public (bool, string?) Create(Delivery delivery)
        {
            try
            {
                db.Deliveries.Add(delivery);
                db.SaveChanges();
                return (true, null);
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }


        public (Delivery?, string?) GetById(int id)
        {
            try
            {
                var res = db.Deliveries.Where(c => c.Delivery_id == id).FirstOrDefault();

                if (res == null)
                {
                    return (null, "Delivery not found");
                }

                return (res, null);
            }
            catch (Exception ex)
            {
                return (null, ex.Message);
            }
        }



        public (bool, string?) Update(int id, string name, string modifiedBy)
        {
            var existing = db.Deliveries.Where(c => c.Delivery_id == id).FirstOrDefault();
            if (existing == null)
                return (false, "Delivery not found");

            existing.UpdateName(name, modifiedBy);
            db.SaveChanges();
            return (true, null);
        }


        public (bool, string?) Delete(int id, string deletedBy)
        {
            try
            {
                var delivery = db.Deliveries.Where(c => c.Delivery_id == id).FirstOrDefault();
                if (delivery == null)
                    return (false, "Delivery not found");

                delivery.Delete(id,deletedBy);
                db.SaveChanges();
                return (true, null);
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        public (List<Delivery>, string?) GetAll()
        {
            try
            {
                var res = db.Deliveries.Where(c => c.IsDeleted == false).ToList();
                return (res, null);
            }
            catch (Exception ex)
            {
                return (new List<Delivery>(), ex.Message);
            }
        }
    }
}
