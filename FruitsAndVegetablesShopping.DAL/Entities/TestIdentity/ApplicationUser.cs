using FruitsAndVegetablesShopping.DAL.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Group7_demo_DAL.Entities
{
    public class ApplicationUser : IdentityUser
    {
       

        
        public virtual Customer? CustomerProfile { get; set; }
        public virtual Admin? AdminProfile { get; set; }
    }
}
