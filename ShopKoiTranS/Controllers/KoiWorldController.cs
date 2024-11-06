using Microsoft.AspNetCore.Mvc;

namespace ShopKoiTranS.Controllers
{
    public class KoiWorldController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult KoiDetails()
        {
            return View();
        }
    }
}
