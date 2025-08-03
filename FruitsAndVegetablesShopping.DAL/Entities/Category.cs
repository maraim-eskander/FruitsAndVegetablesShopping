namespace FruitsAndVegetablesShopping.DAL.Entities
{
    public class Category
    {
        public int CategoryId { get; private set; }
        public string Name { get; private set; }
        public List<Product> Products { get; private set; }
        public DateTime? CreatedOn { get; private set; }
        public string CreatedBy { get; private set; }
        public bool IsDeleted { get; private set; }    
        public DateTime? ModifiedOn { get; private set; }
        public string? ModifiedBy { get; private set; }
        public DateTime? DeletedOn { get; private set; }
        public string? DeletedBy { get; private set; }


        public Category(string name , string createdBy)
        {
            Name = name;
            Products = new List<Product>();
            CreatedOn = DateTime.Now;
            CreatedBy = createdBy;
            IsDeleted = false;
        }

        public Category() { Products = new List<Product>(); }
        public void UpdateName(string name, string modifiedBy)
        {
            Name = name;
            ModifiedOn = DateTime.Now;
            ModifiedBy = modifiedBy;
        }
        public void Delete(string deletedBy)
        {
            IsDeleted = true;
            DeletedOn = DateTime.Now;
            DeletedBy = deletedBy;
        }
    }
}
