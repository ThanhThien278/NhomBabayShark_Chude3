using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ShopKoiTranS.Repository.Components
{
    public class CategoriKoiViewComponent : ViewComponent
    {
        private readonly DataContext _dataContext;

        public CategoriKoiViewComponent(DataContext context)
        {
            _dataContext = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = await _dataContext.LoaiCaKoi.ToListAsync();
            return View(categories); 
        }
    }
}
