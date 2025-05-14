namespace test.Models;

public class Product
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public  string Description { get; set; }
    public required string ImageUrl { get; set; }
    public decimal Price { get; set; }
}
