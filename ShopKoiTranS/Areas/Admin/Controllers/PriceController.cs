using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopKoiTranS.Models;
using ShopKoiTranS.Repository;
using System.Linq;
using System.Threading.Tasks;

namespace ShopKoiTranS.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PriceController : Controller
    {
        private readonly DataContext _context;

        public PriceController(DataContext context)
        {
            _context = context;
        }

        // GET: Price/Index
        public IActionResult Index()
        {
            var prices = _context.Price.ToList();
            foreach (var price in prices)
            {
                switch (price.TransportMethod)
                {
                    case "Air":
                        price.TransportMethod = "Vận Chuyển Hàng Không";
                        break;
                    case "Sea":
                        price.TransportMethod = "Vận Chuyển Hàng Hải";
                        break;
                    case "Land":
                        price.TransportMethod = "Vận Chuyển Đường Bộ";
                        break;
                    default:
                        price.TransportMethod = "Chưa xác định";
                        break;
                }
            }

            return View(prices);
        }

        // GET: Price/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Price/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PriceModel price)
        {
            if (ModelState.IsValid)
            {
                _context.Price.Add(price);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Thêm mới bảng giá thành công!";
                return RedirectToAction(nameof(Index));
            }

            return View(price);
        }

        // GET: Price/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var price = await _context.Price.FindAsync(id);
            if (price == null)
            {
                return NotFound();
            }
            return View(price);
        }

        // POST: Price/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PriceModel price)
        {
            if (id != price.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(price);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Cập nhật bảng giá thành công!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PriceExists(price.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction(nameof(Index));
            }
            return View(price);
        }

        // GET: Price/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var price = await _context.Price.FindAsync(id);
            if (price == null)
            {
                return NotFound();
            }

            return View(price);
        }

        // POST: Price/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var price = await _context.Price.FindAsync(id);
            if (price != null)
            {
                _context.Price.Remove(price); // Xóa dữ liệu
                await _context.SaveChangesAsync(); // Lưu thay đổi vào cơ sở dữ liệu
                TempData["SuccessMessage"] = "Xóa bảng giá thành công!"; // Thông báo xóa thành công
            }

            return RedirectToAction(nameof(Index)); // Quay lại danh sách
        }

        private bool PriceExists(int id)
        {
            return _context.Price.Any(e => e.Id == id);
        }
    }
}