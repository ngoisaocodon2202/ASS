using ASS.Models;

namespace ASS.Data
{
    public static class SeedData
    {
        public static void Initialize(AppDbContext db)
        {
            if (db.Categories.Any()) return;

            // =======================
            // CATEGORY
            // =======================
            var categories = new List<Category>()
            {
                new Category { Name = "Đồ uống" },      // ID = 1
                new Category { Name = "Bánh ngọt" },     // ID = 2
                new Category { Name = "Đồ ăn nhanh" }    // ID = 3
            };

            db.Categories.AddRange(categories);
            db.SaveChanges();

            // =======================
            // FOODS (30 món)
            // =======================

            var foods = new List<Food>();

            // ---- ĐỒ UỐNG (du1 → du10)
            for (int i = 1; i <= 10; i++)
            {
                foods.Add(new Food
                {
                    Name = $"Đồ uống {i}",
                    Price = 15000 + i * 1000,
                    Image = $"/img/du{i}.jpg",
                    CategoryId = 1
                });
            }

            // ---- BÁNH NGỌT (bn1 → bn10)
            for (int i = 1; i <= 10; i++)
            {
                foods.Add(new Food
                {
                    Name = $"Bánh ngọt {i}",
                    Price = 20000 + i * 1500,
                    Image = $"/img/bn{i}.jpg",
                    CategoryId = 2
                });
            }

            // ---- ĐỒ ĂN NHANH (dan1 → dan10)
            for (int i = 1; i <= 10; i++)
            {
                foods.Add(new Food
                {
                    Name = $"Đồ ăn nhanh {i}",
                    Price = 25000 + i * 2000,
                    Image = $"/img/dan{i}.jpg",
                    CategoryId = 3
                });
            }

            db.Foods.AddRange(foods);
            db.SaveChanges();

            // =======================
            // BANNERS
            // =======================
            var banners = new List<Banner>()
            {
                new Banner { Title="Khuyến mãi đồ uống", Image="/img/banner1.jpg" },
                new Banner { Title="Bánh ngọt siêu ngon", Image="/img/banner2.jpg" },
                new Banner { Title="Đồ ăn nhanh cực chất", Image="/img/banner3.jpg" }
            };

            db.Banners.AddRange(banners);
            db.SaveChanges();
        }
    }
}
