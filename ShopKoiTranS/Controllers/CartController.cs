using Microsoft.AspNetCore.Mvc;
using ShopKoiTranS.Models;
using ShopKoiTranS.Repository;
using ShopKoiTranS.Models.ViewModel;
using System.Linq;
using System.Threading.Tasks;

public class CartController : Controller
{
    private readonly DataContext _dataContext;

    public CartController(DataContext context)
    {
        _dataContext = context;
    }

    public IActionResult Index()
    {
        List<CartItemModel> cartItems = HttpContext.Session.GetJson<List<CartItemModel>>("Cart") ?? new List<CartItemModel>();

        CartItemViewModel cartVN = new CartItemViewModel
        {
            Items = cartItems,
            GrandTotal = cartItems.Sum(x => x.Quantity * x.Price),
        };

        return View(cartVN);
    }

    public async Task<IActionResult> Add(int id)
    {
        KoiWorldModel koi = await _dataContext.KoiWorld.FindAsync(id);

        if (koi == null)
        {
            TempData["ErrorMessage"] = "Sản phẩm không tồn tại!";
            return RedirectToAction("Index");
        }

        List<CartItemModel> cartItems = HttpContext.Session.GetJson<List<CartItemModel>>("Cart") ?? new List<CartItemModel>();

        var existingItem = cartItems.FirstOrDefault(x => x.KoiId == koi.KoiId);

        if (existingItem != null)
        {
            existingItem.Quantity += 1;
        }
        else
        {
            cartItems.Add(new CartItemModel
            {
                KoiId = koi.KoiId,
                KoiName = koi.KoiName,
                Price = koi.Price,
                Quantity = 1,
            });
        }

        HttpContext.Session.SetJson("Cart", cartItems);

        TempData["SuccessMessage"] = "Sản phẩm đã được thêm vào giỏ hàng!";
        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult RemoveItem(int productId)
    {
        List<CartItemModel> cartItems = HttpContext.Session.GetJson<List<CartItemModel>>("Cart") ?? new List<CartItemModel>();

        var itemToRemove = cartItems.FirstOrDefault(x => x.KoiId == productId);
        
        if (itemToRemove != null)
        {
            cartItems.Remove(itemToRemove);
            HttpContext.Session.SetJson("Cart", cartItems);

            TempData["SuccessMessage"] = "Sản phẩm đã được xóa khỏi giỏ hàng!";
        }
        else
        {
            TempData["ErrorMessage"] = "Không tìm thấy sản phẩm!";
        }

        return RedirectToAction("Index");
    }
}
