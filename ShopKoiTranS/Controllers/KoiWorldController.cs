using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using ShopKoiTranS.Repository;

namespace ShopKoiTranS.Controllers
{
    public class KoiWorldController : Controller
    {
        private readonly DataContext _context;

        public KoiWorldController(DataContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int categoryId = 0)
        {
            if (categoryId == 0)
            {
                var koiList = await _context.KoiWorld
                                             .OrderByDescending(c => c.KoiId)
                                             .ToListAsync();

                ViewBag.Categories = await _context.LoaiCaKoi.ToListAsync();

                return View(koiList);
            }

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

        public IActionResult KoiDetails(int id)
        {
            var koi = _context.KoiWorld.FirstOrDefault(k => k.KoiId == id);

            if (koi == null)
            {
                return NotFound();
            }

            return View(koi);
        }
    }
}
