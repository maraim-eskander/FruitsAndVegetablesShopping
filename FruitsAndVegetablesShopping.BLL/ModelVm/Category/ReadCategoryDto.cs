namespace FruitsAndVegetablesShopping.BLL.ModelVm.Category
{
    public class ReadCategoryDto
    {
        public string Name { get; set; }
        public List<string> Products { get; set; } = new List<string>();
       
    }
}
