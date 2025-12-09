using ASS.Services;
using ASS.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace ASS.Controllers
{
    public class AuthController : Controller
    {
        private readonly UserService _userService;
        private readonly AuthService _authService;
        private readonly EmailService _emailService;

        public AuthController(UserService userService, AuthService authService, EmailService emailService)
        {
            _userService = userService;
            _authService = authService;
            _emailService = emailService;
        }

        // =============================
        //   REGISTER (GET)
        // =============================
        public IActionResult Register()
        {
            return View();
        }

        // =============================
        //   REGISTER (POST)
        // =============================
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            // Chỉ cho phép Gmail
            if (!model.Email!.EndsWith("@gmail.com"))
            {
                ModelState.AddModelError("", "Vui lòng sử dụng địa chỉ Google Mail (Gmail).");
                return View(model);
            }

            // Kiểm tra email tồn tại
            var exists = await _userService.GetByEmail(model.Email);
            if (exists != null)
            {
                ModelState.AddModelError("", "Email đã tồn tại!");
                return View(model);
            }

            // Tạo mã kích hoạt
            string activationCode = Guid.NewGuid().ToString();

            var user = new ASS.Models.User
            {
                Email = model.Email,
                Password = model.Password,
                FullName = model.FullName,
                ActivationCode = activationCode,
                IsActivated = false
            };

            await _userService.Add(user);

            // Gửi email kích hoạt tài khoản
            string activationLink = Url.Action(
                "Activate",
                "Auth",
                new { code = activationCode },
                Request.Scheme
            )!;

            string body = $@"
                <h3>Xin chào {model.FullName}</h3>
                <p>Bạn vừa đăng ký tài khoản FastFoodStore.</p>
                <p>Nhấn vào link để kích hoạt tài khoản:</p>
                <a href='{activationLink}' style='color:blue;'>Kích hoạt tài khoản</a>
            ";

            _emailService.SendEmail(model.Email, "Kích hoạt tài khoản FastFoodStore", body);

            ViewBag.Message = "Đăng ký thành công, vui lòng kiểm tra Gmail để kích hoạt tài khoản.";

            return View();
        }

        // =============================
        //   ACTIVATE ACCOUNT
        // =============================
        public async Task<IActionResult> Activate(string code)
        {
            await _userService.Activate(code);
            ViewBag.Message = "Kích hoạt tài khoản thành công! Bạn có thể đăng nhập ngay.";
            return View();
        }

        // =============================
        // LOGIN (GET)
        // =============================
        public IActionResult Login()
        {
            return View();
        }

        // =============================
        // LOGIN (POST)
        // =============================
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            // Chỉ cho phép đăng nhập bằng Gmail
            if (!model.Email!.EndsWith("@gmail.com"))
            {
                ModelState.AddModelError("", "Bạn chỉ có thể đăng nhập bằng Google Mail (Gmail).");
                return View(model);
            }

            var user = await _authService.Login(model.Email, model.Password!);

            if (user == null)
            {
                ModelState.AddModelError("", "Sai email, mật khẩu hoặc tài khoản chưa kích hoạt.");
                return View(model);
            }

            // Lưu session user login
            HttpContext.Session.SetString("UserEmail", user.Email!);
            HttpContext.Session.SetString("UserName", user.FullName!);

            return RedirectToAction("Index", "Home");
        }

        // =============================
        // LOGOUT
        // =============================
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
