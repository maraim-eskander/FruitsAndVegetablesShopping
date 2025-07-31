using System.ComponentModel.DataAnnotations;

namespace FruitsAndVegetablesShopping.DAL.Entities
{
    public class Customer
    {
        public int CustomerId { get; private set; }
        [Required]
        public string Name { get; private set; }
        public string Phone { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; } 
        public string? Location { get; private set; }
        public DateTime CreatedOn { get; private set; }
        public string CreatedBy { get; private set; }
        public DateTime? ModifiedOn { get; private set; }
        public string? ModifiedBy { get; private set; }
        public DateTime? DeletedOn { get; private set; }
        public string? DeletedBy { get; private set; }
        public bool? IsDeleted { get; private set; }
        public Customer(int customerId, string name, string phone, string email,string password,string location, string createdBy)
        {
            CustomerId = customerId;
            Name = name;
            Phone = phone;
            Email = email;
            Password = password;
            Location = location;
            CreatedOn = DateTime.Now;
            CreatedBy = createdBy;
            IsDeleted = false;
        }
        public void Delete(int id, string? deletedBy)
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
