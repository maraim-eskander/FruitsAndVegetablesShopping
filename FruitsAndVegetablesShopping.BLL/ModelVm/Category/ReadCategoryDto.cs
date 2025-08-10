namespace FruitsAndVegetablesShopping.BLL.ModelVm.Category
{
    public class ReadCategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<string> Products { get; set; } = new List<string>();
       
    }
}
