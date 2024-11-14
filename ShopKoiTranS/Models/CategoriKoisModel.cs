using System.ComponentModel.DataAnnotations;

namespace ShopKoiTranS.Models
{
    public class CategoriKoisModel
    {
        [Key]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Tên loại cá không được để trống")]
        [StringLength(100, ErrorMessage = "Tên loại cá không được vượt quá 100 ký tự")]
        [Display(Name = "Tên Loại Cá")]
        public string CategoryName { get; set; }

        [StringLength(500, ErrorMessage = "Mô tả không được vượt quá 500 ký tự")]
        [Display(Name = "Mô Tả")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Trạng Thái")]
        public bool IsActive { get; set; }
        public ICollection<KoiWorldModel> KoiWorlds { get; set; }
    }
}
