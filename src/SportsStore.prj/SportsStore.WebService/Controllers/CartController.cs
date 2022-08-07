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
	private readonly Cart _cart;

	public CartController(IProductRepository productRepository, Cart cart)
	{
		_productRepository = productRepository ?? throw new System.ArgumentNullException(nameof(productRepository));
		_cart              = cart ?? throw new System.ArgumentNullException(nameof(cart));
	}

	public RedirectToActionResult AddToCart(int productId, string returnUrl)
	{
		var product = _productRepository.Products.FirstOrDefault(p => p.Id == productId);

		if(product != null) _cart.AddItem(product, 1);

		return RedirectToAction("Index", new { returnUrl });
	}

	public RedirectToActionResult RemoveFromCart(int productId, string returnUrl)
	{
		var product = _productRepository.Products.FirstOrDefault(p => p.Id == productId);

		if(product != null) _cart.RemoveLine(product);

		return RedirectToAction("Index", new { returnUrl });
	}

	public ViewResult Index(string returnUrl)
	{
		var cartIndex = new CartIndexViewModel()
		{
			Cart      = _cart,
			ReturnUrl = returnUrl
		};

		return View(cartIndex);
	}
}
