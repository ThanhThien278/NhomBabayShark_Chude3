namespace ShopKoiTranS.Models
{
    public class PriceModel
    {
        public int Id { get; set; }
        public string TransportMethod { get; set; }
        public string VehicleType { get; set; }
        public int MinWeight { get; set; }
        public int MaxWeight { get; set; }
        public decimal Price0_50 { get; set; }
        public decimal Price51_100 { get; set; }
        public decimal Price101_200 { get; set; }
        public decimal PriceOver200 { get; set; }
        public decimal PriceOver20Kg { get; set; }
    }
}
