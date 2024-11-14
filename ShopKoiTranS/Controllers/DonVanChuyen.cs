using ShopKoiTranS.Models;

namespace ShopKoiTranS.Controllers
{
    internal class DonVanChuyen : TransportModel
    {
        public object CustomerName { get; set; }
        public object CustomerPhone { get; set; }
        public object LoaiCa { get; set; }
        public object TenCa { get; set; }
        public decimal CanNang { get; set; }
        public string DiaDiemXuatPhat { get; set; }
        public string DiaDiemDen { get; set; }
        public string PhuongThucVanChuyen { get; set; }
        public string PhuongTienVanChuyen { get; set; }
        public decimal TransportPrice { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}