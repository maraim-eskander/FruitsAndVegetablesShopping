namespace FruitsAndVegetablesShopping.DAL.Entities
{

public class ProductOrder
{
    [Key]
    public int ProductOrderId { get; private set; }

    [Required, ForeignKey("Order")]
    public int Orderid { get;  set; }

    [Required, ForeignKey("Product")]
    public int Productid { get;  set; }

    public int Quantity { get; private set; }

    [Required]
    public Orders Order { get;  set; }

    [Required]
    public Product Product { get;  set; }

    [Required]
    public DateTime CreatedOn { get; private set; } = DateTime.Now;

    public string? CreatedBy { get; private set; }

    public DateTime? ModifiedOn { get; private set; }

    public string? ModifiedBy { get; private set; }

    public bool isDeleted { get; private set; } = false;

    public DateTime? DeletedOn { get; private set; }

    public string? DeletedBy { get; private set; }

    public void delete(string deletedBy)
    {
        this.isDeleted = true;
        this.DeletedBy = deletedBy;
        this.DeletedOn = DateTime.Now;
    }

    public void edit(int qty)
    {
        this.Quantity = qty;
    }
}
}
