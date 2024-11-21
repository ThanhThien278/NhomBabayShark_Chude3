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
    public class AdminAdviseController : Controller
    {
        private readonly DataContext _context;

        public AdminAdviseController(DataContext context)
        {
            _context = context;
        }

        // GET: Admin/AdminAdvise
        public async Task<IActionResult> Index()
        {
            // Fetch all advises with status "Chờ xác nhận"
            var advises = await _context.LichTuVans
                .Where(a => a.TrangThai == "Chờ xác nhận")
                .OrderByDescending(a => a.ThoiGianTuVan)
                .ToListAsync();

            return View(advises);
        }

        // GET: Admin/AdminAdvise/Confirm/5
        [HttpGet]
        public async Task<IActionResult> Confirm(int id)
        {
            // Fetch the advise record by id
            var advise = await _context.LichTuVans.FindAsync(id);
            if (advise == null)
            {
                return NotFound();
            }

            return View(advise);
        }

        // POST: Admin/AdminAdvise/Confirm/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Confirm(int id, DateTime? consultationTime)
        {
            var advise = await _context.LichTuVans.FindAsync(id);
            if (advise == null)
            {
                return NotFound();
            }

            // Update the status and set the consultation time if provided
            advise.TrangThai = "Đã đăng ký tư vấn thành công";
            advise.ThoiGianTuVan = consultationTime ?? advise.ThoiGianTuVan;

            _context.Update(advise);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Trạng thái tư vấn đã được cập nhật!";
            return RedirectToAction(nameof(Index));
        }
    }
}
