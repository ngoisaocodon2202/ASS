using ASS.Data;
using ASS.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

// Database
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Service DI
builder.Services.AddTransient<EmailService>();     // Gửi mail kích hoạt
builder.Services.AddScoped<UserService>();         // CRUD User
builder.Services.AddScoped<AuthService>();         // Login xử lý
builder.Services.AddScoped<HomeService>();         // HomeService phải là Scoped (bắt buộc)
builder.Services.AddSession();

var app = builder.Build();

app.UseSession();
app.UseStaticFiles();
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

app.Run();
