using Microsoft.AspNetCore.Mvc;
using ShopKoiTranS.Models;
using ShopKoiTranS.Repository;

public class TransportController : Controller
{
    private readonly DataContext _context;

    public TransportController(DataContext context)
    {
        _context = context;
    }

    public decimal CalculateTransportPrice(string phuongThucVanChuyen, string phuongTienVanChuyen, decimal canNang, decimal khoangCach, int soLuongCa)
    {
        phuongThucVanChuyen = phuongThucVanChuyen.ToLower();
        phuongTienVanChuyen = phuongTienVanChuyen.ToLower();

        var priceTable = _context.Price
            .Where(p => p.TransportMethod.ToLower() == phuongThucVanChuyen && p.VehicleType.ToLower() == phuongTienVanChuyen)
            .ToList();

        if (priceTable == null || !priceTable.Any())
        {
            throw new Exception("Không tìm thấy bảng giá cho phương thức và phương tiện vận chuyển này.");
        }

        decimal price = 0;
        foreach (var priceRow in priceTable)
        {
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
        }

        // Tính giá theo khoảng cách
        if (khoangCach <= 50)
        {
            price += priceTable.First().Price0_50;
        }
        else if (khoangCach <= 100)
        {
            price += priceTable.First().Price51_100;
        }
        else if (khoangCach <= 200)
        {
            price += priceTable.First().Price101_200;
        }
        else if (khoangCach > 200)
        {
            price += priceTable.First().PriceOver200;
        }

       
        price *= soLuongCa; 

        return price;
    }



    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }


    [HttpPost]
    public IActionResult Calculate(string customerName, string customerPhone, string loaiCa, string tenCa, decimal canNang, string diaDiemXuatPhat, string diaDiemDen, string phuongThucVanChuyen, string phuongTienVanChuyen, decimal khoangCach, int soLuongCa)
    {
        if (ModelState.IsValid)
        {
 
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
                CreatedAt = DateTime.Now
            };

            _context.DonVanChuyens.Add(newOrder);
            _context.SaveChanges();


            ViewBag.SuccessMessage = "Đặt đơn vận chuyển thành công!";
            ViewBag.TotalPrice = totalPrice;
            ViewBag.CreatedAt = newOrder.CreatedAt;
            ViewBag.OrderDetails = newOrder;

            return View("Index");
        }

        ViewBag.ErrorMessage = "Có lỗi xảy ra, vui lòng kiểm tra lại các thông tin.";
        return View("Index");
    }

}
