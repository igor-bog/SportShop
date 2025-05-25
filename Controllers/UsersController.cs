using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using test.Services;

namespace test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
  public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
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
  var result = await _userService.RegisterUserAsync(user);

    if (result)
        return Ok($"Пользователь {user.FirstName} добавлен.");
    else
        return StatusCode(500, "Ошибка при добавлении пользователя.");
        }
    }
}
