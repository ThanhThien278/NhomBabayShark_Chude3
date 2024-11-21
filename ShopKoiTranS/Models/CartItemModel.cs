using System.Reflection.Metadata.Ecma335;

namespace ShopKoiTranS.Models
{
    public class CartItemModel
    {
        public int KoiId { get; set; }
        public string KoiName { get; set; }
        public string Image { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public decimal TotalPrice
        {
            get { return Price * Quantity; }
        }
        public CartItemModel() 
        { 

        }
        public CartItemModel(int koiId, string koiName, string image, decimal price, int quantity)
        {
            KoiId = koiId;
            KoiName = koiName;
            Image = image;
            Price = price;
            Quantity = quantity;
        }
    }
}
