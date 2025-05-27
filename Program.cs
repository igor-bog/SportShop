using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using test.Services;
using test.Models;



var builder = WebApplication.CreateBuilder(args);



// Add services to the container.
builder.Services.AddControllersWithViews();

// 👇 Добавляем поддержку сессий
builder.Services.AddSession();

builder.Services.AddRazorPages();  // Это добавляет поддержку Razor Pages

builder.Services.AddControllers();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IHomeService, HomeService>();

// Добавляем поддержку сессий
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Время жизни сессии
    options.Cookie.HttpOnly = true;  // Доступ к cookies только через HTTP
    options.Cookie.IsEssential = true; // Cookies обязательны
});


builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=F:\\SQLite Databases/ProjectDb.sqlite"));

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login";
    });


var app = builder.Build();




// Добавление пользователей в таблицу при запуске приложения
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.Migrate();

    // Добавление записи в таблицу users
    if (!dbContext.Users.Any()) // Проверяем, если в таблице нет пользователей
    {
        dbContext.Users.AddRange(
            new User { FirstName = "Alice", SecondName = "Johnson", Password = "password123", Role = "user" },
            new User { FirstName = "Bob", SecondName = "Smith", Password = "qwerty456", Role = "admin" },
            new User { FirstName = "Charlie", SecondName = "Brown", Password = "abcDEF789", Role ="user" }
        );
        dbContext.SaveChanges(); // Сохраняем изменения в базе данных
    }
}




// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.MapRazorPages();  // Чтобы Razor Pages работали

app.UseHttpsRedirection();
app.UseRouting();



app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

//controller functions  :



// Подключаем маршруты для контроллеров
app.MapControllers();  // Это позволяет API-методам из контроллеров работать

app.UseSession(); // 👈 Важно: до UseAuthorization


// Настройка аутентификации
app.UseAuthentication();
app.UseAuthorization();


// Маршруты для Razor Pages
app.MapRazorPages();

builder.Services.AddSession();
app.UseSession();

app.Run();
