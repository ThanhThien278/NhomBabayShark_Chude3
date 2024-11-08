using Microsoft.AspNetCore.Mvc;

namespace ShopKoiTranS.Controllers
{
    public class SalesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
