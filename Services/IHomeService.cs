// Services/IHomeService.cs
using test.Models;

public interface IHomeService
{
    List<User> GetAllUsers();
    void RegisterUser(User user);
    User Authenticate(string firstName, string password);
}
