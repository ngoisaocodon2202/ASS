using ASS.Data;
using Microsoft.EntityFrameworkCore;
using ASS.Models;

namespace ASS.Services
{
    public class AuthService
    {
        private readonly AppDbContext _db;

        public AuthService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<User?> Login(string email, string password)
        {
            return await _db.Users
                .FirstOrDefaultAsync(x => x.Email == email && x.Password == password && x.IsActivated);
        }
    }
}
