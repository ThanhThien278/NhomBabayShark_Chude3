using Microsoft.AspNetCore.Mvc;
using ShopKoiTranS.Models;
using ShopKoiTranS.Repository;
using System;

namespace ShopKoiTranS.Controllers
{
    public class AdviseController : Controller
    {
        private readonly DataContext _context;

        public AdviseController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SubmitAdvise(string customerName, string customerPhone, string diaDiem, string moTa)
        {
            if (ModelState.IsValid)
            {
                // Tạo một bản ghi mới cho tư vấn
                AdviseModel newAdvise = new AdviseModel
                {
                    CustomerName = customerName,
                    CustomerPhone = customerPhone,
                    DiaDiem = diaDiem,
                    MoTa = moTa,
                    TrangThai = "Chờ xác nhận",
                    ThoiGianTuVan = DateTime.Now
                };

                // Thêm vào cơ sở dữ liệu
                _context.LichTuVans.Add(newAdvise);
                _context.SaveChanges();

     
                ViewBag.SuccessMessage = "Đặt lịch tư vấn thành công!";
                ViewBag.AdviseDetails = newAdvise;

                return View("Index");
            }

            // Thông báo lỗi nếu có
            ViewBag.ErrorMessage = "Có lỗi xảy ra, vui lòng kiểm tra lại các thông tin.";
            return View("Index");
        }
    }
}
