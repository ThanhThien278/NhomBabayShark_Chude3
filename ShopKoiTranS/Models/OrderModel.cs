using ShopKoiTranS.Models;
using System.ComponentModel.DataAnnotations;

public class OrderModel
{
    [Key]
    public int Id { get; set; }

    public string UserName { get; set; }

    public DateTime OrderDate { get; set; }

    public decimal GrandTota { get; set; }
    public string Status { get; set; }

    public string PaymentMethod { get; set; }

    public List<OrderDetailModel> OrderDetails { get; set; }


    public int? TransportId { get; set; }
    public TransportModel Transport { get; set; }

    // Tư vấn (nếu có)
    public int? AdviseId { get; set; }
    public AdviseModel Advise { get; set; }

    public OrderModel()
    {
        OrderDetails = new List<OrderDetailModel>();
    }
}