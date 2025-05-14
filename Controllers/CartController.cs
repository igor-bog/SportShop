using Microsoft.AspNetCore.Mvc;
using test.Models; // подставь своё пространство имён
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;

public class CartController : Controller
{
    public IActionResult Index()
    {
        var cart = HttpContext.Session.GetObjectFromJson<List<Product>>("Cart") ?? new List<Product>();
        return View(cart);
    }

    [HttpPost]
    public IActionResult AddToCart(int id, string name, decimal price, string imageUrl)
    {
        var cart = HttpContext.Session.GetObjectFromJson<List<Product>>("Cart") ?? new List<Product>();

        var product = new Product
        {
            Id = id,
            Name = name,
            Price = price,
            ImageUrl = imageUrl
        };

        cart.Add(product);
        HttpContext.Session.SetObjectAsJson("Cart", cart);

        return RedirectToAction("Cart", "Home");
    }
}
