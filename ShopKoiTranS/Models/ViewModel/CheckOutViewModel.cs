namespace ShopKoiTranS.Models.ViewModel
{
    public class CheckoutViewModel
    {
        public List<CartItemModel> Items { get; set; }
        public decimal TotalProductPrice => Items?.Sum(x => x.TotalPrice) ?? 0;
        public decimal TotalTransportPrice { get; set; }
        public decimal GrandTotal => TotalProductPrice + TotalTransportPrice;

        public List<TransportModel> Transports { get; set; }
        public string PaymentMethod { get; set; }
    }
}