using FruitsAndVegetablesShopping.DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Group7_demo_DAL.Entities
{
    public class Customer
    {
        [Key]
        [ForeignKey("User")]
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }
        [Required]
        public string Name { get; private set; }
        //public string Phone { get; private set; }
        //public string Email { get; private set; }
        //public string Password { get; private set; }
        public string? Location { get; private set; }
        public DateTime CreatedOn { get; private set; }
        public string CreatedBy { get; private set; }
        public DateTime? ModifiedOn { get; private set; }
        public string? ModifiedBy { get; private set; }
        public DateTime? DeletedOn { get; private set; }
        public string? DeletedBy { get; private set; }
        public bool? IsDeleted { get; private set; }

        public List<Orders> Orders { get; private set; }

        public Cart Cart { get; private set; }
        public Customer(string userId, string name, string location, string createdBy)
        {
            UserId = userId;
            Name = name;
            Location = location;
            CreatedOn = DateTime.Now;
            CreatedBy = createdBy;
            IsDeleted = false;
        }
        public void Delete(string id, string? deletedBy)
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
