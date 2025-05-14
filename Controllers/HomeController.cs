using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using test.Models;

namespace test.Controllers;

public class HomeController : Controller
{
   private readonly AppDbContext _context; // Объявляем контекст

        // Инжектируем контекст в конструктор
        public HomeController(AppDbContext context)
        {
            _context = context;
        }


           public IActionResult Users()
    {
        var users = _context.Users.ToList();
        return View(users);
    }





          ///////////////////////////////////////////// Отображение корзины
        public IActionResult Cart()
        {
            // Получаем список товаров из сессии (если они есть)
            var cart = HttpContext.Session.GetObjectFromJson<List<Product>>("Cart") ?? new List<Product>();
            return View(cart); // Отправляем товары на представление
        }

        // Добавить товар в корзину
        [HttpPost]
        public IActionResult AddToCart(Product product)
        {
            // Получаем текущую корзину из сессии
            var cart = HttpContext.Session.GetObjectFromJson<List<Product>>("Cart") ?? new List<Product>();
            
            // Добавляем новый товар в корзину
            cart.Add(product);
            
            // Сохраняем обновленную корзину в сессию
            HttpContext.Session.SetObjectAsJson("Cart", cart);
            
            // Перенаправляем на страницу корзины
            return RedirectToAction("Cart", "Home");
        }

        // Очистить корзину
        public IActionResult ClearCart()
        {
            // Удаляем корзину из сессии
            HttpContext.Session.Remove("Cart");
            return RedirectToAction("Cart");
        }
    
    
// ///////////////////////////////////



    public IActionResult Index()
    {
        return View();
    }


public IActionResult Logout()
{

    // Очищаем корзину из сессии
        HttpContext.Session.Remove("Cart");

    // Очистка сессии (если ты используешь сессии)
    HttpContext.Session.Clear();

    // Можно также очистить куки вручную, если они используются
    Response.Cookies.Delete(".AspNetCore.Cookies"); // если используешь cookie auth

    return RedirectToAction("Index", "Home");
}


[HttpPost]
public IActionResult RemoveFromCart(int id)
{
    var cart = HttpContext.Session.GetObjectFromJson<List<Product>>("Cart") ?? new List<Product>();

    // Удаление по ID первого совпавшего
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
    private readonly AppDbContext _context;

    public AdminController(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult Users()
    {
        var users = _context.Users.ToList();
        return View(users);
    }
}







    public IActionResult IndexUser()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }



 public IActionResult Products()
    {
        return View();
    }

 // Страница регистрации
        public IActionResult Register()
        {
            return View();
        }

        // Обработка формы регистрации
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

                _context.Users.Add(user);
                _context.SaveChanges();
                return RedirectToAction("Index"); // Перенаправляем на главную страницу после регистрации
            }

            return View();
        }



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



          // Метод для отображения страницы логина
        public IActionResult Login()
        {
            return View();
        }

        // Метод для обработки логина
        [HttpPost]
        public IActionResult Login(string FirstName, string Password)
        {
            // Поиск пользователя в базе данных
            var user = _context.Users.SingleOrDefault(u => u.FirstName == FirstName && u.Password == Password);
            
            if (user != null)
            {
                // Логика успешной авторизации
                // Например, установка cookies или сессии

              // Сохраняем данные в сессию
    HttpContext.Session.SetString("FirstName", user.FirstName);
    HttpContext.Session.SetString("Role", user.Role ?? "user"); // если вдруг null, будет "user"

              if (user.Role?.ToLower() == "admin")
        {
            return RedirectToAction("AdminPanel", "Home"); // или RedirectToPage, если Razor Pages
        }

                // Пример: редирект на страницу профиля после успешного логина
                return RedirectToAction("Privacy");
            }

            // Если логин не удался
            ViewData["Error"] = "Invalid username or password";
            return View();
        }

//



    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
