using test.Models;
using Microsoft.EntityFrameworkCore;

namespace test.Services
{
    public class AccountService : IAccountService
    {
        private readonly AppDbContext _context;

        public AccountService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<User?> AuthenticateAsync(string firstName, string password)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.FirstName == firstName && u.Password == password);
        }
    }
}
