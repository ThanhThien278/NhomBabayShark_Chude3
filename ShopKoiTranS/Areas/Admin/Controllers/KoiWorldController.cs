using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopKoiTranS.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using ShopKoiTranS.Repository;
using System.Linq;

namespace ShopKoiTranS.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class KoiWorldController : Controller
    {
        private readonly DataContext _datacontext;

        public KoiWorldController(DataContext context)
        {
            _datacontext = context;
        }


        private async Task LoadCategories()
        {
            var categories = await _datacontext.LoaiCaKoi.ToListAsync();
            ViewBag.Loaicakoi = new SelectList(categories, "CategoryId", "CategoryName");
        }


        public async Task<IActionResult> Index()
        {
            var koiWorlds = await _datacontext.KoiWorld
                .Include(k => k.CategoryKoi)
                .OrderByDescending(p => p.KoiId)
                .ToListAsync();
            return View(koiWorlds);
        }


        public async Task<IActionResult> Create()
        {
            await LoadCategories();
            return View();
        }


    
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(KoiWorldModel koiWorld, string NewCategoryName, IFormFile Image)
        {
            if (ModelState.IsValid)
            {

                var duplicateKoi = await _datacontext.KoiWorld.AnyAsync(k => k.KoiName == koiWorld.KoiName);
                if (duplicateKoi)
                {
                    ModelState.AddModelError("KoiName", "Sản phẩm đã tồn tại.");
                    await LoadCategories();
                    return View(koiWorld);
                }


                if (!string.IsNullOrEmpty(NewCategoryName))
                {

                    var existingCategory = await _datacontext.LoaiCaKoi
                        .FirstOrDefaultAsync(c => c.CategoryName.Equals(NewCategoryName, StringComparison.OrdinalIgnoreCase));

                    if (existingCategory != null)
                    {
                        koiWorld.CategoryKoiId = existingCategory.CategoryId;
                    }
                    else
                    {

                        var newCategory = new CategoriKoisModel { CategoryName = NewCategoryName };
                        _datacontext.LoaiCaKoi.Add(newCategory);
                        await _datacontext.SaveChangesAsync();

                        
                        koiWorld.CategoryKoiId = newCategory.CategoryId;
                    }
                }
                else if (koiWorld.CategoryKoiId == 0)
                {
                    ModelState.AddModelError("CategoryKoiId", "Vui lòng chọn hoặc thêm loài cá mới.");
                    await LoadCategories();
                    return View(koiWorld);
                }


                if (Image != null && Image.Length > 0)
                {
                    var filePath = Path.Combine("wwwroot/img", Image.FileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await Image.CopyToAsync(stream);
                    }
                    koiWorld.Image = Image.FileName;
                }


                _datacontext.KoiWorld.Add(koiWorld);
                await _datacontext.SaveChangesAsync();

                TempData["SuccessMessage"] = "Cá Koi đã được thêm thành công!";
                return RedirectToAction(nameof(Index));
            }


            await LoadCategories();
            return View(koiWorld);
        }



        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var koi = await _datacontext.KoiWorld.FindAsync(id);
            if (koi == null)
            {
                return NotFound();
            }

            await LoadCategories();
            return View(koi); 
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, KoiWorldModel updatedKoi, string NewCategoryName, IFormFile Image)
        {
            if (id != updatedKoi.KoiId)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                var koi = await _datacontext.KoiWorld.FindAsync(id);
                if (koi == null)
                {
                    return NotFound();
                }


                if (!string.IsNullOrEmpty(NewCategoryName))
                {
                    var newCategory = new CategoriKoisModel { CategoryName = NewCategoryName };
                    _datacontext.LoaiCaKoi.Add(newCategory);
                    await _datacontext.SaveChangesAsync();
                    updatedKoi.CategoryKoiId = newCategory.CategoryId;
                }


                if (Image != null && Image.Length > 0)
                {
                    var filePath = Path.Combine("wwwroot/img", Image.FileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await Image.CopyToAsync(stream);
                    }
                    updatedKoi.Image = Image.FileName;
                }
                else
                {
                    updatedKoi.Image = koi.Image; 
                }

                koi.KoiName = updatedKoi.KoiName;
                koi.Size = updatedKoi.Size;
                koi.Description = updatedKoi.Description;
                koi.Price = updatedKoi.Price;
                koi.CategoryKoiId = updatedKoi.CategoryKoiId;
                koi.Image = updatedKoi.Image;

                _datacontext.Update(koi);
                await _datacontext.SaveChangesAsync();

                TempData["SuccessMessage"] = "Cập nhật Cá Koi thành công!";

                return RedirectToAction(nameof(Index));
            }

            return Json(new { success = false, message = "Dữ liệu không hợp lệ." });
        }


        public async Task<IActionResult> Delete(int id)
        {
            var koi = await _datacontext.KoiWorld.FindAsync(id);
            if (koi == null)
            {
                return NotFound();
            }

            return View(koi); 
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var koi = await _datacontext.KoiWorld.FindAsync(id);
            if (koi != null)
            {
                _datacontext.KoiWorld.Remove(koi);
                await _datacontext.SaveChangesAsync();
                TempData["SuccessMessage"] = "Xóa Cá Koi thành công!";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
