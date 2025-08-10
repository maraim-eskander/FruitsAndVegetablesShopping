using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Group7_demo_BLL.ModelVM
{
    public class RegisterVM
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Phone { get;  set; }
        [Required]
        public string Email { get;  set; }
        [Required]
        public string Password { get;  set; }

        public string? Location { get; set; }
    }
}
