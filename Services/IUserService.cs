using test.Models;
using System.Threading.Tasks;

namespace test.Services
{
    public interface IUserService
    {
        void RegisterUser(User user);
        Task<bool> RegisterUserAsync(User user);
                
        bool UserExists(string firstName);


    }
}
