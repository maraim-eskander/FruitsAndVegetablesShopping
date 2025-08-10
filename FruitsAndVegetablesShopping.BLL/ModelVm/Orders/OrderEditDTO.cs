using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Group7_demo_BLL.ModelVM
{
    public class OrderEditDTO
    {
        public int OrderId { get; set; }
        public string CustomerId { get; set; }

        

        public List<ProductOrderEditDTO> ProductOrders { get; set; } = new List<ProductOrderEditDTO>();
    }
}
