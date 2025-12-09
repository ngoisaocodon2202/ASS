using ASS.Data;
using ASS.Models;
using Microsoft.EntityFrameworkCore;

namespace ASS.Services
{
    public class UserService
    {
        private readonly AppDbContext _db;

        public UserService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<User?> GetByEmail(string email)
        {
            return await _db.Users.FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task Add(User user)
        {
            await _db.Users.AddAsync(user);
            await _db.SaveChangesAsync();
        }

        public async Task Activate(string code)
        {
            var user = await _db.Users.FirstOrDefaultAsync(x => x.ActivationCode == code);
            if (user != null)
            {
                user.IsActivated = true;
                await _db.SaveChangesAsync();
            }
        }
    }
}
