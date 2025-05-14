using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize(Roles = "admin")]
public class AdminController : Controller
{
    public IActionResult AdminPanel()
    {
          // Проверка роли из сессии
    var role = HttpContext.Session.GetString("Role");

    if (string.IsNullOrEmpty(role) || role != "admin")
    {
        return RedirectToAction("Index", "Home"); // или показать страницу "Access Denied"
    }
        return View();
    }
}
