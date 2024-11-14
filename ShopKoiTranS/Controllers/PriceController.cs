using Microsoft.AspNetCore.Mvc;
using ShopKoiTranS.Models;
using ShopKoiTranS.Repository;
using System.Linq;

namespace ShopKoiTranS.Controllers
{
    public class PriceController : Controller
    {
        private readonly DataContext _context;

        public PriceController(DataContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {

            var prices = _context.Price.ToList();
            foreach (var price in prices)
            {
                switch (price.TransportMethod)
                {
                    case "Air":
                        price.TransportMethod = "Vận Chuyển Hàng Không";
                        break;
                    case "Sea":
                        price.TransportMethod = "Vận Chuyển Hàng Hải";
                        break;
                    case "Land":
                        price.TransportMethod = "Vận Chuyển Đường Bộ";
                        break;
                    default:
                        price.TransportMethod = "Chưa xác định";
                        break;
                }
            }

            return View(prices);
        }
    }
}