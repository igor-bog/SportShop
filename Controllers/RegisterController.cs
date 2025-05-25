using Microsoft.AspNetCore.Mvc;
using test.Models;
using test.Services;

namespace test.Controllers;

[ApiController]
[Route("[controller]")]
public class RegisterController : ControllerBase
{
     private readonly IUserService _userService;

        public RegisterController(IUserService userService)
        {
            _userService = userService;
        }

    [HttpPost]
    public IActionResult Post([FromBody] User user)
    {
         _userService.RegisterUser(user);
        return Ok(new { success = true });
    }
}
