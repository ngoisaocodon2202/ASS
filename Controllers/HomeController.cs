using Microsoft.AspNetCore.Mvc;
using ASS.Services;
using ASS.ViewModels;

namespace ASS.Controllers
{
    public class HomeController : Controller
    {
        private readonly HomeService _homeService;

        public HomeController(HomeService homeService)
        {
            _homeService = homeService;
        }

        // ========================================
        // CATEGORY PAGE (10 sản phẩm mỗi trang)
        // ========================================
        public async Task<IActionResult> Category(int id, int page = 1)
        {
            int pageSize = 10;

            var allFoods = await _homeService.GetFoods();
            var filtered = allFoods.Where(x => x.CategoryId == id).ToList();

            int totalItems = filtered.Count();
            int totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            var foods = filtered
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var categoryName = id switch
            {
                1 => "Đồ uống",
                2 => "Bánh ngọt",
                3 => "Đồ ăn nhanh",
                _ => "Thực đơn"
            };

            var model = new CategoryViewModel
            {
                Foods = foods,
                CategoryName = categoryName,
                CategoryId = id,
                CurrentPage = page,
                TotalPages = totalPages
            };

            return View("Category", model);
        }

        // ========================================
        // HOME PAGE
        // ========================================
        public async Task<IActionResult> Index()
        {
            ViewBag.UserName = HttpContext.Session.GetString("UserName");

            var model = new HomeViewModel
            {
                Banners = await _homeService.GetBanners(),
                Foods = await _homeService.GetFoods(),
                Categories = await _homeService.GetCategories()
            };

            return View(model);
        }
    }
}
