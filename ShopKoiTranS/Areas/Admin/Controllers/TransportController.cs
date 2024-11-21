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
    public class TransportController : Controller
    {
        private readonly DataContext _context;

        public TransportController(DataContext context)
        {
            _context = context;
        }

        // Hiển thị danh sách các đơn hàng vận chuyển
        public async Task<IActionResult> Index()
        {
            var transports = await _context.DonVanChuyens
                .Where(t => t.TransportPrice > 0)
                .OrderByDescending(t => t.CreatedAt)
                .ToListAsync();

            if (transports.Count == 0)
            {
                TempData["NoTransportsMessage"] = "Không có đơn hàng vận chuyển nào!";
            }

            return View(transports);
        }

        // Xem chi tiết đơn hàng
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var transport = await _context.DonVanChuyens
                .Include(t => t.User)
                .FirstOrDefaultAsync(t => t.TransportId == id);

            if (transport == null)
            {
                return NotFound();
            }

            return View(transport);
        }

        // Xác nhận đơn hàng
        [HttpGet]
        public async Task<IActionResult> Confirm(int id)
        {
            var transport = await _context.DonVanChuyens.FindAsync(id);
            if (transport == null)
            {
                return NotFound();
            }

            return View(transport); // Hiển thị form xác nhận đơn hàng
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Confirm(int id, DateTime? deliveryDate)
        {
            var transport = await _context.DonVanChuyens.FindAsync(id);
            if (transport == null)
            {
                return NotFound();
            }

            if (deliveryDate == null || deliveryDate <= DateTime.Now)
            {
                ModelState.AddModelError("DeliveryDate", "Ngày giao hàng phải lớn hơn ngày hiện tại.");
                return View(transport);
            }

            transport.TransportPrice *= 1.1m; // Ví dụ tăng giá vì lý do vận hành
            transport.CreatedAt = deliveryDate.Value;

            _context.Update(transport);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Đơn hàng đã được xác nhận thành công!";
            return RedirectToAction(nameof(Index));
        }
    }
}