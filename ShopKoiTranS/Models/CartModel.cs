using System.ComponentModel.DataAnnotations;

public class CartModel
{
    [Key]
    public int CartId { get; set; }
    public string UserName { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? UpdatedAt { get; set; }
    public List<CartItemModel> Items { get; set; } = new List<CartItemModel>();
    public decimal TotalAmount
    {
        get
        {
            return Items?.Sum(item => item.TotalPrice) ?? 0;
        }
        set { }
    }
}
