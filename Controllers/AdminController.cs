using ASS.Data;
using ASS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASS.Controllers
{
    public class AdminController : Controller
    {
        private readonly AppDbContext _db;

        public AdminController(AppDbContext db)
        {
            _db = db;
        }

        // ====================================
        // LIST + PAGING
        // ====================================
        public async Task<IActionResult> Index(int page = 1)
        {
            int pageSize = 10;

            var totalItems = await _db.Foods.CountAsync();
            var foods = await _db.Foods
                .Include(x => x.Category)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            return View(foods);
        }

        // ====================================
        // CREATE (GET)
        // ====================================
        public IActionResult Create()
        {
            ViewBag.Categories = _db.Categories.ToList();
            return View();
        }

        // ====================================
        // CREATE (POST)
        // ====================================
        [HttpPost]
        public async Task<IActionResult> Create(Food food)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = _db.Categories.ToList();
                return View(food);
            }

            await _db.Foods.AddAsync(food);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // ====================================
        // EDIT (GET)
        // ====================================
        public async Task<IActionResult> Edit(int id)
        {
            var food = await _db.Foods.FindAsync(id);
            if (food == null) return NotFound();

            ViewBag.Categories = _db.Categories.ToList();
            return View(food);
        }

        // ====================================
        // EDIT (POST)
        // ====================================
        [HttpPost]
        public async Task<IActionResult> Edit(Food model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = _db.Categories.ToList();
                return View(model);
            }

            _db.Foods.Update(model);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // ====================================
        // DELETE (GET)
        // ====================================
        public async Task<IActionResult> Delete(int id)
        {
            var food = await _db.Foods.FindAsync(id);
            if (food == null) return NotFound();

            return View(food);
        }

        // ====================================
        // DELETE (POST)
        // ====================================
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var food = await _db.Foods.FindAsync(id);

            if (food != null)
            {
                _db.Foods.Remove(food);
                await _db.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }
    }
}
