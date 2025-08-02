using System.ComponentModel.DataAnnotations;

namespace FruitsAndVegetablesShopping.DAL.Entities
{
    public class Orders
{
    public int Order_id { get; private set; }

    public float totalPrice { get; private set; }

    public int numOfItems { get; private set; }

    [ForeignKey("customer")]
    public int Customer_id { get; private set; }

    public Customer customer { get; private set; }

    [ForeignKey("delivery")]
    public int Delivery_id { get; private set; }

    public Delivery delivery { get; private set; }

    public List<ProductOrder> ProductOrders { get; private set; } = new List<ProductOrder>();

    [Required]
    public DateTime CreatedOn { get; private set; } = DateTime.Now;

    public string? CreatedBy { get; private set; }

    public DateTime? ModifiedOn { get; private set; }

    public string? ModifiedBy { get; private set; }

    public bool isDeleted { get; private set; } = false;

    public DateTime? DeletedOn { get; private set; }

    public string? DeletedBy { get; private set; }

    public Orders(float totalPrice, int numOfItems, int customer_id, int delivery_id, List<ProductOrder> productOrders)
    {
        
        this.totalPrice = totalPrice;
        this.numOfItems = numOfItems;
        Delivery_id = delivery_id;
        ProductOrders = productOrders;
        this.CreatedOn = DateTime.Now;
    }

    public void delete(string deletedBy)
    {
        this.isDeleted = true;
        this.DeletedOn = DateTime.Now;
        this.DeletedBy = deletedBy;
        if (ProductOrders != null)
        {
            foreach (var po in ProductOrders)
            {
                po.delete(deletedBy);
            }
        }

    }

    public void edit(float totalprice, int num)
    {
        this.totalPrice = totalprice;
        this.numOfItems = num;
    }
}
}
