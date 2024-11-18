using Microsoft.Extensions.Configuration.UserSecrets;
using System.ComponentModel.DataAnnotations;

namespace ShopKoiTranS.Models
{
    public class FeedbackModel
    {
        [Key]
        public int Id { get; set; }

        public string CustomerName { get; set; } 
        public int Rating { get; set; }  
        public string FeedbackText { get; set; }  
        public DateTime CreatedAt { get; set; }
        public string UserName { get; set; }
        public AppUserModel User { get; set; }
    }
}
