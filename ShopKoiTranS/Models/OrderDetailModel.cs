using System.ComponentModel.DataAnnotations;

namespace ShopKoiTranS.Models
{
    public class OrderDetailModel
    {
        [Key]
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int KoiId { get; set; }
        public string KoiName { get; set; }
        public string Image { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public int? TransportId { get; set; }
        public TransportModel Transport { get; set; }

        public int? AdviseId { get; set; }
        public AdviseModel Advise { get; set; }

        public OrderModel Order { get; set; }
    }
}