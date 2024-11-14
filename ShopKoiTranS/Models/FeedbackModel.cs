namespace ShopKoiTranS.Models
{
    public class FeedbackModel
    {
        public int Id { get; set; }
        public string CustomerName { get; set; } 
        public int Rating { get; set; }  
        public string FeedbackText { get; set; }  
        public DateTime CreatedAt { get; set; }  
    }
}
