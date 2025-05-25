using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using test.Models;
using test.Services;

namespace test.Controllers;

public class HomeController : Controller
{
    private readonly IHomeService _homeService;

    public HomeController(IHomeService homeService)
    {
        _homeService = homeService;
    }

    public IActionResult Users()
    {
        var users = _homeService.GetAllUsers();
        return View(users);
    }

    [Authorize]
    public IActionResult Cart()
    {
        var cart = HttpContext.Session.GetObjectFromJson<List<Product>>("Cart") ?? new List<Product>();
        return View(cart);
    }

    [HttpPost]
    public IActionResult AddToCart(Product product)
    {
        var cart = HttpContext.Session.GetObjectFromJson<List<Product>>("Cart") ?? new List<Product>();
        cart.Add(product);
        HttpContext.Session.SetObjectAsJson("Cart", cart);
        return RedirectToAction("Cart", "Home");
    }

    public IActionResult ClearCart()
    {
        HttpContext.Session.Remove("Cart");
        return RedirectToAction("Cart");
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Logout()
    {
        HttpContext.Session.Remove("Cart");
        HttpContext.Session.Clear();
        Response.Cookies.Delete(".AspNetCore.Cookies");
        return RedirectToAction("Index", "Home");
    }

    [HttpPost]
    public IActionResult RemoveFromCart(int id)
    {
        var cart = HttpContext.Session.GetObjectFromJson<List<Product>>("Cart") ?? new List<Product>();
        var itemToRemove = cart.FirstOrDefault(p => p.Id == id);
        if (itemToRemove != null)
        {
            cart.Remove(itemToRemove);
            HttpContext.Session.SetObjectAsJson("Cart", cart);
        }
        return RedirectToAction("Cart");
    }


  public class AdminController : Controller
    {
        private readonly IHomeService _homeService;

        public AdminController(IHomeService homeService)
        {
            _homeService = homeService;
        }

        public IActionResult Users()
        {
            var users = _homeService.GetAllUsers();
            return View(users);
        }
    }


    [Authorize]
    public IActionResult IndexUser()
    {
        return View();
    }

    [Authorize]
    public IActionResult Privacy()
    {
        return View();
    }

    [Authorize]
    public IActionResult Products()
    {
        return View();
    }

    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Register(RegisterModel model)
    {
        if (ModelState.IsValid)
        {
            var user = new User
            {
                FirstName = model.FirstName,
                SecondName = model.SecondName,
                Password = model.Password,
                Role = "user"
            };

            _homeService.RegisterUser(user);
            return RedirectToAction("Index");
        }

        return View();
    }

    public IActionResult AdminPanel()
    {
        var role = HttpContext.Session.GetString("Role");
        if (string.IsNullOrEmpty(role) || role != "admin")
        {
            return RedirectToAction("Index", "Home");
        }
        return View();
    }

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(string FirstName, string Password)
    {
        var user = _homeService.Authenticate(FirstName, Password);

        if (user != null)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.FirstName),
                new Claim(ClaimTypes.Role, user.Role ?? "user")
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties { IsPersistent = true };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties
            );

            HttpContext.Session.SetString("FirstName", user.FirstName);
            HttpContext.Session.SetString("Role", user.Role ?? "user");

            if (user.Role?.ToLower() == "admin")
                return RedirectToAction("AdminPanel", "Home");

            return RedirectToAction("Privacy");
        }

        ViewData["Error"] = "Invalid username or password";
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
