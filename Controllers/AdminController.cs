using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using test.Services;
using Microsoft.AspNetCore.Http;

[Authorize(Roles = "admin")]
public class AdminController : Controller
{
    private readonly IHomeService _homeService;

    public AdminController(IHomeService homeService)
    {
        _homeService = homeService;
    }

    public IActionResult AdminPanel()
    {
        // Проверка роли из сессии
        var role = HttpContext.Session.GetString("Role");
        if (string.IsNullOrEmpty(role) || role != "admin")
        {
            return RedirectToAction("ErrorAccess", "Home");
        }

        return View();
    }

    public IActionResult Users()
    {
        // Та же проверка
        var role = HttpContext.Session.GetString("Role");
        if (string.IsNullOrEmpty(role) || role != "admin")
        {
            return RedirectToAction("ErrorAccess", "Home");
        }

        var users = _homeService.GetAllUsers();
        return View(users);
    }
}
