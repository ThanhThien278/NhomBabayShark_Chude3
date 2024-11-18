using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopKoiTranS.Models;
using ShopKoiTranS.Repository;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ShopKoiTranS.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdviseController : Controller
    {
        private readonly DataContext _context;

        public AdviseController(DataContext context)
        {
            _context = context;
        }

        // GET: Admin/Advise
        public async Task<IActionResult> Index()
        {
            var advises = await _context.LichTuVans
                .Where(a => a.TrangThai == "Chờ xác nhận" || a.TrangThai == "Đã xác nhận" || a.TrangThai == "Đã hoàn thành")
                .OrderByDescending(a => a.ThoiGianTuVan)
                .ToListAsync();

            if (advises.Count == 0)
            {
                TempData["NoAdvisesMessage"] = "Không có lịch hẹn nào cần xác nhận!";
            }

            return View(advises);
        }

        // GET: Admin/Advise/Confirm/5
        [HttpGet]
        public async Task<IActionResult> Confirm(int id)
        {
            var advise = await _context.LichTuVans.FindAsync(id);
            if (advise == null)
            {
                return NotFound();
            }

            return View(advise); // Trả về view Confirm.cshtml để xác nhận tư vấn
        }

        // POST: Admin/Advise/Confirm/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Confirm(int id, DateTime? consultationTime)
        {
            var advise = await _context.LichTuVans.FindAsync(id);
            if (advise == null)
            {
                return NotFound();
            }

            if (consultationTime == null || consultationTime <= DateTime.Now)
            {
                ModelState.AddModelError("ThoiGianTuVan", "Thời gian tư vấn phải lớn hơn thời gian hiện tại.");
                return View(advise); // Trả về view với thông báo lỗi
            }

            advise.TrangThai = "Đã xác nhận";
            advise.ThoiGianTuVan = consultationTime.Value;

            _context.Update(advise);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Lịch tư vấn đã được xác nhận thành công!";
            return RedirectToAction(nameof(Index));
        }
    }
}
