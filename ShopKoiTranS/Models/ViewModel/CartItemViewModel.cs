namespace ShopKoiTranS.Models.ViewModel
{
    public class CartViewModel
    {
        public List<CartItemModel> Items { get; set; }
        public List<AdviseModel> Advises { get; set; }
        public List<TransportModel> Transports { get; set; }

        public decimal TotalProductPrice => Items?.Sum(x => x.TotalPrice) ?? 0;
        public decimal TotalTransportPrice => Transports?.Sum(x => x.TransportPrice) ?? 0;
        public decimal GrandTotal => TotalProductPrice + TotalTransportPrice;

        public CartViewModel()
        {
            Items = new List<CartItemModel>();
            Advises = new List<AdviseModel>();
            Transports = new List<TransportModel>();
        }
    }
}