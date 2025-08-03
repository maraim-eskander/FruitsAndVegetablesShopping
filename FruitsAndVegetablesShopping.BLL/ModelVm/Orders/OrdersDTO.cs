using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FruitsAndVegetablesShopping.BLL.ModelVm.Category
{
    public class OrdersDTO
    {
        public int OrderId { get; set; }

        public float TotalPrice { get; set; }

        public int NumOfItems { get; set; }

       
        public int CustomerId { get; set; } 

        public int DeliveryId { get; set; }

        

        public List<ProductOrderDTO> ProductOrders { get; set; } = new List<ProductOrderDTO>();
    }
}
