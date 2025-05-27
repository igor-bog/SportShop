// Services/IHomeService.cs
using test.Models;

public interface IHomeService
{

    void DeleteUser(int id);
    List<User> GetAllUsers();
    void RegisterUser(User user);
    User Authenticate(string firstName, string password);
    
    bool UserExists(string firstName);

    
}
