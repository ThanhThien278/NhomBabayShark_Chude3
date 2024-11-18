using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShopKoiTranS.Models.ViewModel;
using ShopKoiTranS.Models;
using ShopKoiTranS.Repository;
using Microsoft.EntityFrameworkCore;

public class CartController : Controller
{
    private readonly DataContext _dataContext;
    private readonly UserManager<AppUserModel> _userManager;

    public CartController(DataContext dataContext, UserManager<AppUserModel> userManager)
    {
        _dataContext = dataContext;
        _userManager = userManager;
    }

    public async Task<IActionResult> Index()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            TempData["Notification"] = "Bạn cần đăng nhập để xem giỏ hàng.";
            return RedirectToAction("Login", "Account");
        }

        var cart = await _dataContext.Carts
                                     .Where(c => c.UserName == user.UserName)
                                     .Include(c => c.Items)
                                     .FirstOrDefaultAsync();

        if (cart == null)
        {
            TempData["Notification"] = "Giỏ hàng của bạn đang trống!";
            return View(new CartViewModel());
        }

        CartViewModel cartVM = new CartViewModel
        {
            Items = cart.Items.Select(i => new CartItemModel
            {
                KoiId = i.KoiId,
                KoiName = i.KoiName,
                Image = i.Image,
                Price = i.Price,
                Quantity = i.Quantity
            }).ToList()
        };

        cartVM.Advises = await _dataContext.LichTuVans
                                            .Where(a => a.UserName == user.UserName)
                                            .ToListAsync();
        cartVM.Transports = await _dataContext.DonVanChuyens
                                              .Where(t => t.UserName == user.UserName)
                                              .ToListAsync();

        return View(cartVM);
    }

    public async Task<IActionResult> Add(int id)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            TempData["Notification"] = "Bạn cần đăng nhập để thêm sản phẩm vào giỏ hàng.";
            return RedirectToAction("Login", "Account");
        }

        var koi = await _dataContext.KoiWorld.FindAsync(id);
        if (koi == null) return RedirectToAction("Index");

        var cart = await _dataContext.Carts
                                     .Where(c => c.UserName == user.UserName)
                                     .Include(c => c.Items)
                                     .FirstOrDefaultAsync();

        if (cart == null)
        {
            cart = new CartModel
            {
                UserName = user.UserName
            };
            _dataContext.Carts.Add(cart);
            await _dataContext.SaveChangesAsync();
        }

        var existingItem = cart.Items.FirstOrDefault(x => x.KoiId == koi.KoiId);
        if (existingItem != null)
        {
            existingItem.Quantity += 1;
        }
        else
        {
            cart.Items.Add(new CartItemModel
            {
                KoiId = koi.KoiId,
                KoiName = koi.KoiName,
                Image = koi.Image,
                Price = koi.Price,
                Quantity = 1
            });
        }

        await _dataContext.SaveChangesAsync();
        TempData["Notification"] = "Sản phẩm đã được thêm vào giỏ hàng!";
        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> UpdateQuantity(int productId, int quantity)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return Json(new { success = false, message = "Bạn cần đăng nhập để thay đổi số lượng." });
        }

        var cart = await _dataContext.Carts
                                     .Where(c => c.UserName == user.UserName)
                                     .Include(c => c.Items)
                                     .FirstOrDefaultAsync();

        if (cart != null)
        {
            var itemToUpdate = cart.Items.FirstOrDefault(i => i.KoiId == productId);
            if (itemToUpdate != null && quantity > 0)
            {
                itemToUpdate.Quantity = quantity;
                await _dataContext.SaveChangesAsync();

                return Json(new
                {
                    success = true,
                    message = "Số lượng đã được cập nhật."
                });
            }
            else
            {
                return Json(new { success = false, message = "Số lượng không hợp lệ." });
            }
        }

        return Json(new { success = false, message = "Giỏ hàng không tồn tại." });
    }

    public async Task<IActionResult> RemoveItem(int cartId, int koiId)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return Json(new { success = false, message = "Bạn cần đăng nhập để xóa sản phẩm." });
        }

        var cart = await _dataContext.Carts
                                .Where(c => c.UserName == user.UserName)
                                .Include(c => c.Items)
                                .FirstOrDefaultAsync();

        if (cart == null)
        {
            return Json(new { success = false, message = "Giỏ hàng không tồn tại." });
        }

        var itemToRemove = cart.Items.FirstOrDefault(i => i.KoiId == koiId);
        if (itemToRemove == null)
        {
            return Json(new { success = false, message = "Sản phẩm không tồn tại trong giỏ hàng." });
        }

        _dataContext.CartItems.Remove(itemToRemove);

        var adviseToRemove = await _dataContext.LichTuVans
                                            .Where(a => a.UserName == user.UserName)
                                            .FirstOrDefaultAsync();
        if (adviseToRemove != null)
        {
            _dataContext.LichTuVans.Remove(adviseToRemove);
        }

        var transportToRemove = await _dataContext.DonVanChuyens
                                              .Where(t => t.UserName == user.UserName)
                                              .FirstOrDefaultAsync();
        if (transportToRemove != null)
        {
            _dataContext.DonVanChuyens.Remove(transportToRemove);
        }

        cart.TotalAmount = cart.Items.Sum(i => i.Price * i.Quantity);

        if (!cart.Items.Any())
        {
            _dataContext.Carts.Remove(cart);
        }

        await _dataContext.SaveChangesAsync();

        return Json(new { success = true, message = "Sản phẩm đã được xóa khỏi giỏ hàng!", newCartTotal = cart.TotalAmount });
    }

    [HttpPost]
    public async Task<IActionResult> PlaceOrder()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            TempData["Notification"] = "Bạn cần đăng nhập để đặt đơn hàng.";
            return RedirectToAction("Login", "Account");
        }

        var cart = await _dataContext.Carts
                                     .Where(c => c.UserName == user.UserName)
                                     .Include(c => c.Items)
                                     .FirstOrDefaultAsync();

        if (cart == null || cart.Items.Count == 0)
        {
            TempData["Notification"] = "Giỏ hàng của bạn đang trống!";
            return RedirectToAction("Index");
        }

        OrderModel newOrder = new OrderModel
        {
            UserName = user.UserName,
            OrderDate = DateTime.Now,
            TotalAmount = cart.Items.Sum(i => i.Price * i.Quantity),
            Status = "Pending",
        };

        await _dataContext.Orders.AddAsync(newOrder);
        await _dataContext.SaveChangesAsync();

        foreach (var item in cart.Items)
        {
            OrderDetailModel detail = new OrderDetailModel
            {
                OrderId = newOrder.Id,
                KoiId = item.KoiId,
                KoiName = item.KoiName,
                Image = item.Image,
                Price = item.Price,
                Quantity = item.Quantity
            };
            await _dataContext.OrderDetails.AddAsync(detail);
        }

        await _dataContext.SaveChangesAsync();

        _dataContext.Carts.Remove(cart);
        await _dataContext.SaveChangesAsync();

        TempData["Notification"] = "Đơn hàng của bạn đã được đặt thành công!";
        return RedirectToAction("Index", "Home");
    }
}
