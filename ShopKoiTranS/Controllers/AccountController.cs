using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShopKoiTranS.Models;
using System.Threading.Tasks;

namespace ShopKoiTranS.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUserModel> _userManager;
        private readonly SignInManager<AppUserModel> _signInManager;

        public AccountController(UserManager<AppUserModel> userManager, SignInManager<AppUserModel> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // GET: Account/Index (Trang đăng nhập)
        public IActionResult Index()
        {
            return View(new UserModel());
        }

        // POST: Account/Index (Xử lý đăng nhập)
        [HttpPost]
        public async Task<IActionResult> Index(UserModel model)
        {
            if (ModelState.IsValid)
            {
                // Lấy người dùng từ UserManager bằng tên đăng nhập
                var user = await _userManager.FindByNameAsync(model.Username);
                if (user != null)
                {
                    // Kiểm tra mật khẩu và đăng nhập
                    var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                // Thêm lỗi nếu đăng nhập thất bại
                ModelState.AddModelError(string.Empty, "Tên đăng nhập hoặc mật khẩu không đúng.");
            }
            return View(model);
        }


        // GET: Account/Register (Trang đăng ký)
        public IActionResult Register()
        {
            return View(new UserModel());
        }

        // POST: Account/Register (Xử lý đăng ký)
        [HttpPost]
        public async Task<IActionResult> Register(UserModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new AppUserModel { UserName = model.Username, Email = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    // Nếu đăng ký thành công, chuyển đến trang đăng nhập
                    return RedirectToAction("Index");
                }
                // Nếu có lỗi, thêm vào ModelState để hiển thị
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }

        // POST: Account/Logout
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            TempData["LogoutMessage"] = "Đăng xuất thành công!";
            return RedirectToAction("Index", "Home");
        }
    }
}
