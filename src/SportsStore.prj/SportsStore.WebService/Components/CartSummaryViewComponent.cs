using Microsoft.AspNetCore.Mvc;
using SportsStore.WebService.Models;

namespace SportsStore.WebService.Components;

public class CartSummaryViewComponent : ViewComponent
{
	private readonly Cart _cart;

	public CartSummaryViewComponent(Cart cart)
	{
		_cart = cart ?? throw new System.ArgumentNullException(nameof(cart));
	}

	public IViewComponentResult Invoke() => View(_cart);
}
