using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Linq;
using test.Models;

namespace test.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;

        public AccountController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string FirstName, string Password)
        {
            var user = _context.Users.FirstOrDefault(u => u.FirstName == FirstName && u.Password == Password);
            if (user != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.FirstName),
                    new Claim("FullName", $"{user.FirstName} {user.SecondName}"),
                     new Claim(ClaimTypes.Role, user.Role) // Используем роль из базы данных!
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);


             // 🔁 Перенаправляем по роли:

Console.WriteLine($"user.Role: '{user.Role}'");


    if (user.Role == "admin")
    {

  Console.WriteLine("Redirecting to Admin Panel");


        return RedirectToAction("AdminPanel", "Home");
    }
    else
    {


                return RedirectToAction("Index", "Home");
            }
        }
            ViewBag.Error = "Invalid username or password";
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
    }
}
