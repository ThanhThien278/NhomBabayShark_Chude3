using System.ComponentModel.DataAnnotations;

public class CartItemModel
{
    [Key]
    public int CartItemId { get; set; }
    public int KoiId { get; set; }
    public string KoiName { get; set; }
    public string Image { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
  
    public int CartId { get; set; }
    public CartModel Cart { get; set; }
    public decimal TotalPrice
    {
        get
        {
            return Price * Quantity;
        }
    }
}
