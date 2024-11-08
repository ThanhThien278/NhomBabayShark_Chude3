using Microsoft.AspNetCore.Mvc;

namespace ShopKoiTranS.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

    }
}
