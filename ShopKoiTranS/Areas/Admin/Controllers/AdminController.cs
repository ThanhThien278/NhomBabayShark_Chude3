using Microsoft.AspNetCore.Mvc;

namespace ShopKoiTranS.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
