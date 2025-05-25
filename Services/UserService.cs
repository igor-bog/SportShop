using test.Models;
using System.Threading.Tasks;

namespace test.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context    = context;

        }



        public async Task<bool> RegisterUserAsync(User user)
        {
            if (string.IsNullOrWhiteSpace(user.FirstName) ||
                string.IsNullOrWhiteSpace(user.SecondName) ||
                string.IsNullOrWhiteSpace(user.Password))
            {
                return false;
            }

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return true;
        }
        
  public void RegisterUser(User user)
    {
        _context.Users.Add(user);
        _context.SaveChanges();
    }

    }
}
