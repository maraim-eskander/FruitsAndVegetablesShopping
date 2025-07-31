using System.ComponentModel.DataAnnotations;

namespace FruitsAndVegetablesShopping.DAL.Entities
{
    public class Admin
    {
        public int AdminId { get; private set; }
        [Required]
        public string Name { get; private set; }
        public string Phone { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public DateTime CreatedOn { get; private set; }
        public string CreatedBy { get; private set; }
        public DateTime? ModifiedOn { get; private set; }
        public string? ModifiedBy { get; private set; }
        public DateTime? DeletedOn { get; private set; }
        public string? DeletedBy { get; private set; }
        public bool? IsDeleted { get; private set; }
        public Admin(int adminId, string name, string phone, string email, string password, string createdBy)
        {
            AdminId = adminId;
            Name = name;
            Phone = phone;
            Email = email;
            Password = password;
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
