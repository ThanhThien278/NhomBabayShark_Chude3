using Microsoft.AspNetCore.Mvc;
using ShopKoiTranS.Models;
using ShopKoiTranS.Repository;
using System;
using Microsoft.AspNetCore.Identity;

namespace ShopKoiTranS.Controllers
{
    public class AdviseController : Controller
    {
        private readonly DataContext _context;
        private readonly UserManager<AppUserModel> _userManager;

        public AdviseController(DataContext context, UserManager<AppUserModel> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SubmitAdvise(string customerName, string customerPhone, string diaDiem, string moTa)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Lấy tên người dùng từ session hoặc Identity
                    var currentUserName = User.Identity.Name;  // Đây là cách lấy UserName của người dùng đang đăng nhập.

                    if (string.IsNullOrEmpty(currentUserName))
                    {
                        ViewBag.ErrorMessage = "Bạn cần đăng nhập để đặt lịch tư vấn.";
                        return View("Index");
                    }

                    // Tạo một bản ghi mới cho tư vấn
                    AdviseModel newAdvise = new AdviseModel
                    {
                        CustomerName = customerName,
                        CustomerPhone = customerPhone,
                        DiaDiem = diaDiem,
                        MoTa = moTa,
                        TrangThai = "Chờ xác nhận",
                        ThoiGianTuVan = DateTime.Now,
                        UserName = currentUserName // Lưu tên người dùng vào tư vấn
                    };

                    // Thêm vào cơ sở dữ liệu
                    _context.LichTuVans.Add(newAdvise);
                    await _context.SaveChangesAsync();

                    ViewBag.SuccessMessage = "Đặt lịch tư vấn thành công!";
                    ViewBag.AdviseDetails = newAdvise;

                    return View("Index");
                }
                catch (Exception ex)
                {
                    ViewBag.ErrorMessage = "Có lỗi xảy ra: " + ex.Message;
                    return View("Index");
                }
            }

            // Thông báo lỗi nếu có
            ViewBag.ErrorMessage = "Có lỗi xảy ra, vui lòng kiểm tra lại các thông tin.";
            return View("Index");
        }
    }
}
