using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopKoiTranS.Models;
using ShopKoiTranS.Repository;

namespace ShopKoiTranS.Controllers
{
    public class CategoriKoisController : Controller
    {
        private readonly DataContext _context;


        public CategoriKoisController(DataContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int categoryId)
        {
            var categori = await _context.LoaiCaKoi
                                            .Where(c => c.CategoryId == categoryId)
                                            .FirstOrDefaultAsync();

            if (categori == null)
                return RedirectToAction("Index", "Home");

            var KoiWorldByCategory = _context.KoiWorld
                                              .Where(c => c.CategoryKoiId == categoryId);

            ViewBag.Categories = await _context.LoaiCaKoi.ToListAsync();

            return View(await KoiWorldByCategory.OrderByDescending(c => c.KoiId)
                                                .ToListAsync());
        }

    }
}
