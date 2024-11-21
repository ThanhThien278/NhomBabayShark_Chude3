using System;
using System.ComponentModel.DataAnnotations;

namespace ShopKoiTranS.Models
{
    public class AdviseModel
    {
        [Key]
        public int AdviseId { get; set; }

        [Required]
        [Display(Name = "Họ và Tên")]
        public string CustomerName { get; set; }

        [Required]
        [Phone]
        [Display(Name = "Số Điện Thoại")]
        public string CustomerPhone { get; set; }

        [Display(Name = "Thời gian tư vấn")]
        public DateTime? ThoiGianTuVan { get; set; }

        [Required]
        [Display(Name = "Địa Điểm")]
        public string DiaDiem { get; set; }

        [Required]
        [Display(Name = "Mô Tả")]
        public string MoTa { get; set; }

        [Required]
        public string TrangThai { get; set; }
    }
}
