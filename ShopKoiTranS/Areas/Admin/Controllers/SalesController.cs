using Microsoft.AspNetCore.Mvc;
using ShopKoiTranS.Models;
using ShopKoiTranS.Repository;
using System.IO;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ShopKoiTranS.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SalesController : Controller
    {
        private readonly DataContext _context;

        public SalesController(DataContext context)
        {
            _context = context;
        }

        // GET: Sales/Index
        public async Task<IActionResult> Index()
        {
            var salesList = await _context.MemberCars.ToListAsync();
            return View(salesList);
        }

        // GET: Sales/Create
        
        // GET: Sales/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var sales = await _context.MemberCars.FindAsync(id);
            if (sales == null)
            {
                return NotFound();
            }

            return View(sales);
        }

        // POST: Sales/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SalesModel updatedSales, IFormFile Image)
        {
            if (id != updatedSales.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                var sales = await _context.MemberCars.FindAsync(id);
                if (sales == null)
                {
                    return NotFound();
                }

                // Xử lý ảnh mới
                if (Image != null && Image.Length > 0)
                {
                    var filePath = Path.Combine("wwwroot/img", Image.FileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await Image.CopyToAsync(stream);
                    }
                    updatedSales.ImageUrl = Image.FileName;
                }
                else
                {
                    updatedSales.ImageUrl = sales.ImageUrl; // Giữ nguyên ảnh cũ nếu không có ảnh mới
                }

                // Cập nhật thông tin sales
                sales.Title = updatedSales.Title;
                sales.Condition = updatedSales.Condition;
                sales.Benefits = updatedSales.Benefits;
                sales.ImageUrl = updatedSales.ImageUrl;

                _context.Update(sales);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Cập nhật sản phẩm thành công!";
                return RedirectToAction(nameof(Index));
            }

            return View(updatedSales); // Trả về view với lỗi nếu có
        }

        // GET: Sales/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var sales = await _context.MemberCars.FindAsync(id);
            if (sales == null)
            {
                return NotFound();
            }

            return View(sales);
        }

        // POST: Sales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sales = await _context.MemberCars.FindAsync(id);
            if (sales != null)
            {
                _context.MemberCars.Remove(sales);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Xóa sản phẩm thành công!";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
