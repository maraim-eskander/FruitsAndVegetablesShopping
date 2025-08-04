namespace FruitsAndVegetablesShopping.DAL.Entities
{
    public class Product
    {
        public int ProductId { get; private set; }

        public string Name { get; private set; }

        public double Price { get; private set; }
        public string? Image { get; private set; }

        public int Stock { get; private set; }
        public string Description { get; private set; }
        public int CategoryId { get; private set; }
        public Category Category { get; private set; }
        public DateTime? CreatedOn { get; private set; }
        public string CreatedBy { get; private set; }
        public bool IsDeleted { get; private set; }
        public DateTime? ModifiedOn { get; private set; }
        public string? ModifiedBy { get; private set; }
        public DateTime? DeletedOn { get; private set; }
        public string? DeletedBy { get; private set; }

        public Product(string name, double price, string image, int stock, string desc, int categoryId   , string createdBy)
        {
            Name = name;
            Price = price;
            Image = image;
            Stock = stock;
            Description = desc;
            CategoryId = categoryId;
            CreatedOn = DateTime.Now;
            CreatedBy = createdBy;
            IsDeleted = false;
        }
        public void UpdateStock(int newStock, string modifiedBy)
        {
            Stock = newStock;
            ModifiedBy = modifiedBy;
            ModifiedOn = DateTime.Now;
        }

        public Product() { }
        public void Update(string name, double price, string image, int stock, string desc, int categoryId  , string modifiedBy)
        {
            Name = name;
            Price = price;
            Image = image;
            Stock = stock;
            Description = desc;
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
