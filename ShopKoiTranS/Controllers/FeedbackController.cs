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


    public IActionResult Index()
    {
        return View();
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Index(FeedbackModel feedback)
    {
        if (ModelState.IsValid)
        {
            feedback.CreatedAt = DateTime.Now;
            _context.Add(feedback);
            await _context.SaveChangesAsync();


            TempData["SuccessMessage"] = "Cảm ơn bạn đã gửi feedback!";
            return RedirectToAction(nameof(Index));  
        }

        return View(feedback);
    }
}
