// Services/HomeService.cs
using test.Models;

public class HomeService : IHomeService
{
    private readonly AppDbContext _context;

    public HomeService(AppDbContext context)
    {
        _context = context;
    }

    public List<User> GetAllUsers()
    {
        return _context.Users.ToList();
    }

    public void RegisterUser(User user)
    {
        _context.Users.Add(user);
        _context.SaveChanges();
    }

    public User Authenticate(string firstName, string password)
    {
        return _context.Users
            .SingleOrDefault(u => u.FirstName == firstName && u.Password == password);
    }
}
