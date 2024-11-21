using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopKoiTranS.Models;
using ShopKoiTranS.Repository;
using System.Threading.Tasks;

namespace ShopKoiTranS.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class FeedbackController : Controller
    {
        private readonly DataContext _datacontext;

        public FeedbackController(DataContext context)
        {
            _datacontext = context;
        }

        // GET: Feedback/Index
        public async Task<IActionResult> Index()
        {
            var feedbacks = await _datacontext.Feedbacks.OrderByDescending(s => s.Id).ToListAsync();
            return View(feedbacks); // Gửi danh sách feedbacks từ SalesModel tới view
        }

        // GET: Feedback/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var feedback = await _datacontext.Feedbacks.FindAsync(id);
            if (feedback == null)
            {
                return NotFound();
            }

            return View(feedback); // Trả về view để xác nhận xóa feedback
        }

        // POST: Feedback/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var feedback = await _datacontext.Feedbacks.FindAsync(id);
            if (feedback != null)
            {
                _datacontext.Feedbacks.Remove(feedback);
                await _datacontext.SaveChangesAsync();
                TempData["SuccessMessage"] = "Xóa feedback thành công!";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}