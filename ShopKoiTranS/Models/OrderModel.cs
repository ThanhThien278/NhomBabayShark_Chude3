using ShopKoiTranS.Models;
using System.ComponentModel.DataAnnotations;

public class OrderModel
{

    [Key]
    public int Id { get; set; }
    public string UserName { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }
    public string Status { get; set; }

    public List<OrderDetailModel> OrderDetails { get; set; }
    public List<TransportModel> Transports { get; set; }
    public List<AdviseModel> Advises { get; set; }
}
