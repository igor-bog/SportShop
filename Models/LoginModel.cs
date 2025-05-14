namespace test.Models
{
    public class LoginModel
    {
        public required string Username { get; set; }   // Имя пользователя
        public required string Password { get; set; }   // Пароль
        public required bool RememberMe { get; set; }   // Опция "Запомнить меня"
    }
}
