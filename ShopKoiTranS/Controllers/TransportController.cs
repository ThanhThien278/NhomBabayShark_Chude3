using Microsoft.AspNetCore.Mvc;
using ShopKoiTranS.Models;
using ShopKoiTranS.Repository;
using Microsoft.AspNetCore.Identity;

public class TransportController : Controller
{
    private readonly DataContext _context;
    private readonly UserManager<AppAdminModel> _userManager;

    public TransportController(DataContext context, UserManager<AppAdminModel> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public decimal CalculateTransportPrice(string phuongThucVanChuyen, string phuongTienVanChuyen, decimal canNang, decimal khoangCach, int soLuongCa)
    {
        phuongThucVanChuyen = phuongThucVanChuyen.ToLower();
        phuongTienVanChuyen = phuongTienVanChuyen.ToLower();

        var priceTable = _context.Price
            .Where(p => p.TransportMethod.ToLower() == phuongThucVanChuyen && p.VehicleType.ToLower() == phuongTienVanChuyen)
            .ToList();

        if (!priceTable.Any())
        {
            throw new Exception("Không tìm thấy bảng giá cho phương thức và phương tiện vận chuyển này.");
        }

        decimal price = 0;
        var priceRow = priceTable.First();  // Giả sử lấy giá trị của dòng đầu tiên

        // Tính giá theo cân nặng
        if (canNang >= 1 && canNang <= 5)
        {
            price = priceRow.Price0_50;
        }
        else if (canNang >= 6 && canNang <= 10)
        {
            price = priceRow.Price51_100;
        }
        else if (canNang >= 11 && canNang <= 20)
        {
            price = priceRow.Price101_200;
        }
        else if (canNang > 20)
        {
            price = priceRow.PriceOver20Kg;
        }

        // Tính giá theo khoảng cách
        if (khoangCach <= 50)
        {
            price += priceRow.Price0_50;
        }
        else if (khoangCach <= 100)
        {
            price += priceRow.Price51_100;
        }
        else if (khoangCach <= 200)
        {
            price += priceRow.Price101_200;
        }
        else if (khoangCach > 200)
        {
            price += priceRow.PriceOver200;
        }

        // Tính tổng giá với số lượng cá
        price *= soLuongCa;

        return price;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Calculate(string customerName, string customerPhone, string loaiCa, string tenCa, decimal canNang, string diaDiemXuatPhat, string diaDiemDen, string phuongThucVanChuyen, string phuongTienVanChuyen, decimal khoangCach, int soLuongCa)
    {
        if (ModelState.IsValid)
        {
            try
            {
               
                var currentUserName = User.Identity.Name;  

                if (string.IsNullOrEmpty(currentUserName))
                {
                    ViewBag.ErrorMessage = "Bạn cần đăng nhập để đặt đơn vận chuyển.";
                    return View("Index");
                }

                decimal totalPrice = CalculateTransportPrice(phuongThucVanChuyen, phuongTienVanChuyen, canNang, khoangCach, soLuongCa);

                
                TransportModel newOrder = new TransportModel
                {
                    CustomerName = customerName,
                    CustomerPhone = customerPhone,
                    LoaiCa = loaiCa,
                    TenCa = tenCa,
                    CanNang = canNang,
                    DiaDiemXuatPhat = diaDiemXuatPhat,
                    DiaDiemDen = diaDiemDen,
                    PhuongThucVanChuyen = phuongThucVanChuyen,
                    PhuongTienVanChuyen = phuongTienVanChuyen,
                    Distance = khoangCach,
                    TransportPrice = totalPrice,
                    CreatedAt = DateTime.Now,
                    UserName = currentUserName 
                };

                _context.DonVanChuyens.Add(newOrder);
                await _context.SaveChangesAsync();

                ViewBag.SuccessMessage = "Đặt đơn vận chuyển thành công!";
                ViewBag.TotalPrice = totalPrice;
                ViewBag.CreatedAt = newOrder.CreatedAt;
                ViewBag.OrderDetails = newOrder;

                return View("Index");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View("Index");
            }
        }

        ViewBag.ErrorMessage = "Có lỗi xảy ra, vui lòng kiểm tra lại các thông tin.";
        return View("Index");
    }
}
