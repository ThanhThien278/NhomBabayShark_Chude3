namespace ShopKoiTranS.Models
{
    public class SalesModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Condition { get; set; }
        public List<string> Benefits { get; set; }
        public string ImageUrl { get; set; }
    }
}
