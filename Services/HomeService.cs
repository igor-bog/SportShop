// Services/HomeService.cs
using test.Models;

public class HomeService : IHomeService
{
    private readonly AppDbContext _context;

    public HomeService(AppDbContext context)
    {
        _context = context;
    }

        public void DeleteUser(int id)
    {
        var user = _context.Users.Find(id);
        if (user != null)
        {
            _context.Users.Remove(user);
            _context.SaveChanges(); // ← именно это удаляет из базы
        }
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


        public bool UserExists(string firstName)
{
    return _context.Users.Any(u => u.FirstName == firstName);
}


    public User Authenticate(string firstName, string password)
    {
        return _context.Users
            .SingleOrDefault(u => u.FirstName == firstName && u.Password == password);
    }
}
