using System.ComponentModel.DataAnnotations;

namespace FruitsAndVegetablesShopping.DAL.Entities
{
    public class Orders
    {
        public int Order_id { get; private set; }

        public float totalPrice { get; private set; }

        public int numOfItems { get; private set; }
        [Required]
        public  Delivery delivery { get; private set; }

        [Required]
        public DateTime CreatedOn { get; private set; } = DateTime.Now;

        public string? CreatedBy { get; private set; }

        public DateTime? ModifiedOn { get; private set; }

        public string? ModifiedBy { get; private set; }

        public bool isDeleted { get; private set; } = false;

        public DateTime? DeletedOn { get; private set; }

        public string? DeletedBy { get; private set; }

        public void delete()
        {
            this.isDeleted = true;
            this.DeletedOn = DateTime.Now;

        }

        public void edit(float totalprice, int num)
        {
            this.totalPrice = totalprice;
            this.numOfItems = num;
        }
    }
}
