using Microsoft.AspNetCore.Mvc;
using ShopKoiTranS.Models;
using ShopKoiTranS.Repository;
using System.Diagnostics;

namespace ShopKoiTranS.Controllers
{
    public class HomeController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, DataContext context)
        {
            _logger = logger;
            _dataContext = context;  
        }


        public IActionResult Index()
        {

            ViewBag.IsAuthenticated = User.Identity.IsAuthenticated;

            if (!User.Identity.IsAuthenticated)
            {
                TempData["Message"] = "Vui lòng đăng nhập để tiếp tục.";
                TempData["MessageType"] = "warning"; 
            }

            return View();
        }



        public IActionResult GetKoiWorld()
        {
            var koiWorld = _dataContext.KoiWorld.ToList();
            return View(koiWorld);  
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(int statuscode)
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
