using Google;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShopKoiTranS.Models;
using ShopKoiTranS.Models.ViewModel;
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

        checkoutViewModel.TotalTransportPrice = checkoutViewModel.Transports.Sum(t => t.TransportPrice);

        return View(checkoutViewModel);
    }


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


            var cartItems = _context.CartItems.Where(c => c.Cart.UserName == user.UserName).ToList();
            _context.CartItems.RemoveRange(cartItems);
            _context.SaveChanges();


            return RedirectToAction("Confirmation");
        }

        return View("Index", model);
    }


    public IActionResult Confirmation()
    {
        return View();
    }
}