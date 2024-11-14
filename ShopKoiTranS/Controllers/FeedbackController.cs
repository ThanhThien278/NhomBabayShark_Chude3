using Microsoft.AspNetCore.Mvc;
using ShopKoiTranS.Models;
using ShopKoiTranS.Repository;

public class FeedbackController : Controller
{
    private readonly DataContext _context;

    public FeedbackController(DataContext context)
    {
        _context = context;
    }

    // GET: Feedback
    public IActionResult Index()
    {
        return View();
    }

    // POST: Feedback
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Index(FeedbackModel feedback)
    {
        if (ModelState.IsValid)
        {
            feedback.CreatedAt = DateTime.Now;
            _context.Add(feedback);
            await _context.SaveChangesAsync();

            // Gửi thông báo thành công đến View
            TempData["SuccessMessage"] = "Cảm ơn bạn đã gửi feedback!";
            return RedirectToAction(nameof(Index));  // Chuyển về trang Index sau khi gửi thành công
        }

        return View(feedback);
    }
}
