using Microsoft.AspNetCore.Mvc;
using test.Models;


namespace test.Controllers;

[ApiController]
[Route("[controller]")]
public class RegisterController : ControllerBase
{
    private readonly AppDbContext _context;

    public RegisterController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public IActionResult Post([FromBody] User user)
    {
        _context.Users.Add(user);
        _context.SaveChanges();
        return Ok(new { success = true });
    }
}
