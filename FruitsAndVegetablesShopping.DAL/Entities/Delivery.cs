

namespace FruitsAndVegetablesShopping.DAL.Entities
{
    public class Delivery
    {
        

        public int Delivery_id { get; private set; }
        public string Name { get; private set; }
        public string Phone { get; private set; }
        public string status { get; private set; }
        public string Destination { get; private set; }
        public DateTime Estimated_time { get; private set; }
        public List<Orders> orders { get; private set; }
        public DateTime? CreatedOn { get; private set; }
        public string CreatedBy { get; private set; }
        public DateTime? ModifiedOn { get; private set; }
        public string? ModifiedBy { get; private set; }
        public DateTime? DeletedOn { get; private set; }
        public string? DeletedBy { get; private set; }
        public bool? IsDeleted { get; private set; }

        public Delivery(int delivery_id, string name, string status, string destination, DateTime estimated_time, string createdBy, DateTime? modifiedOn, string? modifiedBy)
        {
            Delivery_id = delivery_id;
            Name = name;
            this.status = status;
            Destination = destination;
            Estimated_time = estimated_time;
            CreatedOn = DateTime.Now;
            CreatedBy = createdBy;
            ModifiedOn = modifiedOn;
            ModifiedBy = modifiedBy;
            IsDeleted = false;
        }
        public void Delete(int id , string? deletedBy)
        {
            IsDeleted = true;
            DeletedOn = DateTime.Now;
            DeletedBy = deletedBy;
        }
        public void UpdateName(string name, string modifiedBy)
        {
            Name = name;
            ModifiedOn = DateTime.Now;
            ModifiedBy = modifiedBy;
        }

    }
}
