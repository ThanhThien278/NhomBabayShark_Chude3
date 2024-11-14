using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopKoiTranS.Models
{
    public class KoiWorldModel
    {
        [Key]
        public int KoiId { get; set; }

        [Required(ErrorMessage = "Tên cá không được để trống")]
        [Display(Name = "Tên Cá")]
        public string KoiName { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Giá cá phải lớn hơn 0")]
        [Display(Name = "Giá Cá")]
        public decimal Price { get; set; }

        [Display(Name = "Ảnh Cá")]
        public string Image { get; set; }

        [Required(ErrorMessage = "Mô tả cá không được để trống")]
        [StringLength(500, ErrorMessage = "Mô tả không được vượt quá 500 ký tự")]
        [Display(Name = "Mô Tả Cá")]
        public string Description { get; set; }

        [Display(Name = "Loại Cá")]
        public int CategoryKoiId { get; set; }


        [Required(ErrorMessage = "Kích thước cá không được để trống")]
        [Range(0.1, double.MaxValue, ErrorMessage = "Kích thước cá phải lớn hơn 0")]
        [Display(Name = "Kích Thước Cá (cm)")]
        public decimal Size { get; set; }
        [NotMapped]
        [FileExtensions]
        public IFormFile ImageUpLoad { get; set; }
        public CategoriKoisModel CategoryKoi { get; set; }
    }
}
