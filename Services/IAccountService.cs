using test.Models;
using System.Threading.Tasks;

namespace test.Services
{
    public interface IAccountService
    {
        Task<User?> AuthenticateAsync(string firstName, string password);
    }
}
