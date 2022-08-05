using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using SportsStore.WebService.Database;
using SportsStore.WebService.Models;
using System.Linq;
using SportsStore.WebService.Infrastructure;
using SportsStore.WebService.Models.ViewModels;

namespace SportsStore.WebService.Controllers;
public class CartController : Controller
{
    private readonly IProductRepository _productRepository;

    public CartController(IProductRepository productRepository)
    {
        _productRepository = productRepository ?? throw new System.ArgumentNullException(nameof(productRepository));
    }

    public RedirectToActionResult AddToCart(int productId, string returnUrl)
    {
        var product = _productRepository.Products.FirstOrDefault(p => p.Id == productId);

        if(product != null)
        {
            var cart = GetCart();
            cart.AddItem(product, 1);
            SaveCart(cart);
        }

        return RedirectToAction("Index", new { returnUrl });
    }

    public RedirectToActionResult RemoveFromCart(int productId, string returnUrl)
    {
        var product = _productRepository.Products.FirstOrDefault(p => p.Id == productId);

        if(product != null)
        {
            var cart = GetCart();
            cart.RemoveLine(product);
            SaveCart(cart);
        }

        return RedirectToAction("Index", new { returnUrl });
    }

    public ViewResult Index(string returnUrl)
    {
        var cartIndex = new CartIndexViewModel()
        {
            Cart      = GetCart(),
            ReturnUrl = returnUrl
        };

        return View(cartIndex);
    }

    private Cart GetCart() => HttpContext.Session.GetJson<Cart>("Cart") ?? new Cart();

    public void SaveCart(Cart cart) => HttpContext.Session.SetJson("Cart", cart);
}
