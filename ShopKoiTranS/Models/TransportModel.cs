using System;
using System.ComponentModel.DataAnnotations;

namespace ShopKoiTranS.Models
{
    public class TransportModel
    {
        [Key]
        public int TransportId { get; set; }

        [Required]
        [Display(Name = "Họ và Tên")]
        public string CustomerName { get; set; }

        [Required]
        [Display(Name = "Số Điện Thoại")]
        [RegularExpression(@"(\+84|0)[3|5|7|8|9][0-9]{8}", ErrorMessage = "Số điện thoại không hợp lệ.")]
        public string CustomerPhone { get; set; }

        [Required]
        [Display(Name = "Loại Cá")]
        public string LoaiCa { get; set; }

        [Required]
        [Display(Name = "Tên Cá")]
        public string TenCa { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Cân nặng phải lớn hơn 0.")]
        [Display(Name = "Cân Nặng (kg)")]
        public decimal CanNang { get; set; }

        [Required]
        [Display(Name = "Số Lượng Cá")]
        [Range(1, int.MaxValue, ErrorMessage = "Số lượng cá phải lớn hơn 0.")]
        public int SoLuongCa { get; set; }

        [Required]
        [Display(Name = "Địa Điểm Xuất Phát")]
        public string DiaDiemXuatPhat { get; set; }

        [Required]
        [Display(Name = "Địa Điểm Đến")]
        public string DiaDiemDen { get; set; }

        [Required]
        [Display(Name = "Phương Thức Vận Chuyển")]
        public string PhuongThucVanChuyen { get; set; }

        [Required]
        [Display(Name = "Phương Tiện Vận Chuyển")]
        public string PhuongTienVanChuyen { get; set; }

        [Required]
        [Display(Name = "Khoảng Cách (km)")]
        [Range(1, int.MaxValue, ErrorMessage = "Số lượng cá phải lớn hơn 0.")]
        public decimal Distance { get; set; }
        public string TrangThai { get; set; }
        public decimal TransportPrice { get; set; }
        public DateTime CreatedAt { get; set; }
        public string UserName { get; set; }
        public AppUserModel User { get; set; }
    }
}