using ShopKoiTranS.Models;
using System.ComponentModel.DataAnnotations;

public class CartModel
{
    [Key]
    public int CartId { get; set; }
    public string UserName { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? UpdatedAt { get; set; }
    public List<CartItemModel> Items { get; set; } = new List<CartItemModel>();
    public List<TransportModel> Transports { get; set; } = new List<TransportModel>();
    public List<AdviseModel> Advises { get; set; } = new List<AdviseModel>();

    public decimal TotalAmount
    {
        get
        {
            return Items?.Sum(item => item.TotalPrice) ?? 0;
        }
        set { }
    }

    public decimal TotalTransportPrice
    {
        get
        {
            return Transports?.Sum(transport => transport.TransportPrice) ?? 0;
        }
        set { }
    }

   

    public decimal GrandTotal
    {
        get
        {
            return TotalAmount + TotalTransportPrice ;
        }
    }
}
