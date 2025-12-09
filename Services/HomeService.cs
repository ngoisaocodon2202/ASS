using ASS.Data;
using ASS.Models;
using Microsoft.EntityFrameworkCore;

namespace ASS.Services
{
    public class HomeService
    {
        private readonly AppDbContext _db;

        public HomeService(AppDbContext db)
        {
            _db = db;
        }

        // Lấy danh sách banner
        public async Task<List<Banner>> GetBanners()
        {
            return await _db.Banners.ToListAsync();
        }

        // Lấy danh sách món ăn
        public async Task<List<Food>> GetFoods()
        {
            return await _db.Foods.Include(x => x.Category).ToListAsync();
        }

        // Lấy danh sách thực đơn
        public async Task<List<Category>> GetCategories()
        {
            return await _db.Categories.ToListAsync();
        }
    }
}
