using Microsoft.AspNetCore.Mvc;
using ShopKoiTranS.Models;
using ShopKoiTranS.Repository;
using System.Linq; 

namespace ShopKoiTranS.Controllers
{
    public class SalesController : Controller
    {
        private readonly DataContext _context;

       
        public SalesController(DataContext context)
        {
            _context = context;
        }

     
        public IActionResult Index()
        {

            var salesModels = _context.MemberCars.ToList();

            return View(salesModels);
        }
    }
}