using Microsoft.AspNetCore.Mvc;

namespace ShopKoiTranS.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
