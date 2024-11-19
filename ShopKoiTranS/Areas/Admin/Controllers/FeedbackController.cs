﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopKoiTranS.Models;
using ShopKoiTranS.Repository;
using System.Threading.Tasks;

namespace ShopKoiTranS.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class FeedbackController : Controller
    {
        private readonly DataContext _datacontext;

        public FeedbackController(DataContext context)
        {
            _datacontext = context;
        }


        public async Task<IActionResult> Index()
        {
            var feedbacks = await _datacontext.Feedbacks.OrderByDescending(s => s.Id).ToListAsync();
            return View(feedbacks); 
        }



        public async Task<IActionResult> Delete(int id)
        {
            var feedback = await _datacontext.Feedbacks.FindAsync(id);
            if (feedback == null)
            {
                return NotFound();
            }

            _datacontext.Feedbacks.Remove(feedback);
            await _datacontext.SaveChangesAsync();

            TempData["SuccessMessage"] = "Feedback đã được xóa thành công!";
            return RedirectToAction(nameof(Index));
        }
    }
}
