using Microsoft.AspNetCore.Http;

namespace FruitsAndVegetablesShopping.BLL.ModelVm.Product
{
    public class UpdateProductDto
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string? Image { get; set; }
        public int Stock { get; set; }
        public string Description { get; set; }
       
        public string ModifiedBy { get; set; }
        public IFormFile? File { get; set; }
    }
}
