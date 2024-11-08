using Microsoft.AspNetCore.Mvc;

namespace ShopKoiTranS.Controllers
{
    public class FeedbackController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
