using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShopKoiTranS.Models.ViewModel;
using ShopKoiTranS.Models;
using ShopKoiTranS.Repository;

public class CheckoutController : Controller
{
    private readonly DataContext _context;
    private readonly UserManager<AppUserModel> _userManager;

    public CheckoutController(DataContext context, UserManager<AppUserModel> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    // GET: Checkout
    public async Task<IActionResult> Index()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return RedirectToAction("Login", "Account");
        }

        var checkoutViewModel = new CheckoutViewModel
        {
            Items = _context.CartItems.Where(c => c.Cart.UserName == user.UserName).ToList(),
            Transports = _context.DonVanChuyens.Where(t => t.UserName == user.UserName).ToList()
        };


        var totalTransportPrice = checkoutViewModel.Transports.Sum(t => t.TransportPrice);
        var grandTotal = checkoutViewModel.Items.Sum(i => i.TotalPrice) + totalTransportPrice;

        // Gửi grandTotal và totalTransportPrice đến View để hiển thị
        ViewData["GrandTotal"] = grandTotal;
        ViewData["TotalTransportPrice"] = totalTransportPrice;

        return View(checkoutViewModel);
    }

    // POST: Process Checkout
    [HttpPost]
    public async Task<IActionResult> ProcessCheckout(CheckoutViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            // Tạo đơn hàng mới
            var order = new OrderModel
            {
                UserName = user.UserName,
                OrderDate = DateTime.Now,
                GrandTota = model.GrandTotal,
                Status = "Cho xu li",
                PaymentMethod = model.PaymentMethod
            };


            foreach (var item in model.Items)
            {
                var orderDetail = new OrderDetailModel
                {
                    KoiId = item.KoiId,
                    KoiName = item.KoiName,
                    Price = item.Price,
                    Quantity = item.Quantity,
                    GrandTota = item.Quantity * item.Price
                };
                order.OrderDetails.Add(orderDetail);
            }




            _context.Orders.Add(order);
            await _context.SaveChangesAsync();


            var cartItems = _context.CartItems.Where(c => c.Cart.UserName == user.UserName).ToList();
            _context.CartItems.RemoveRange(cartItems);
            await _context.SaveChangesAsync();


            TempData["OrderSuccessMessage"] = "Đơn hàng đã được tạo thành công!";/ -strong1 / -strong / -strong / -heart:>:o: -((: -h return RedirectToAction("Confirmation");
        }

        return View("Index", model);
    }

    // GET: Checkout Confirmation
    public IActionResult Confirmation()
    {
        return View();
    }
}