using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _db;

        public UsersController(AppDbContext db)
        {
            _db = db;
        }

        // POST: api/users/register
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] User user)
        {
            // Простейшая валидация
            if (string.IsNullOrWhiteSpace(user.FirstName) || 
                string.IsNullOrWhiteSpace(user.SecondName) || 
                string.IsNullOrWhiteSpace(user.Password))
            {
                return BadRequest("Все поля обязательны для заполнения.");
            }

            _db.Users.Add(user);
            await _db.SaveChangesAsync();

            return Ok($"Пользователь {user.FirstName} добавлен.");
        }
    }
}
