namespace test.Models;
public class User
{
    public int Id { get; set; }
    public required string FirstName { get; set; }
    public required string SecondName { get; set; }
    public required string Password { get; set; }
    public required string Role {get; set;}

}
