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

         public void DeleteUser(int id)
    {
        var user = _context.Users.Find(id);
        if (user != null)
        {
            _context.Users.Remove(user);
            _context.SaveChanges(); 
        }
    }
    
public bool UserExists(string firstName)
{
    return _context.Users.Any(u => u.FirstName == firstName);
}



  public List<User> GetAllUsers()
        {
            return _context.Users.ToList();
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
