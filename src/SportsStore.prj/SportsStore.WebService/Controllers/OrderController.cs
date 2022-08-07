using Microsoft.AspNetCore.Mvc;
using SportsStore.WebService.Database;
using SportsStore.WebService.Models;
using System.Linq;

namespace SportsStore.WebService.Controllers;
public class OrderController : Controller
{
	private readonly IOrderRepository _orderRepository;
	private readonly Cart _cart;

	public OrderController(IOrderRepository orderRepository, Cart cart)
	{
		_orderRepository = orderRepository ?? throw new System.ArgumentNullException(nameof(orderRepository));
		_cart = cart ?? throw new System.ArgumentNullException(nameof(cart));
	}
	public IActionResult Checkout() => View(new Order());

	[HttpPost]
	public IActionResult Checkout(Order order)
	{
		if(_cart.Lines.Count() == 0)
		{
			ModelState.AddModelError("", "Sorry, your cart is empty");
		}

		if(!ModelState.IsValid)
		{
			return View(order);
		}

		order.Lines = _cart.Lines.ToArray();
		_orderRepository.SaveOrder(order);
		return RedirectToAction(nameof(Completed));
	}

	public ViewResult List()
	{
		var orders = _orderRepository.Orders.Where(o => o.Shipped == false);
		return View(orders);
	}

	[HttpPost]
	public IActionResult MarkShipped(int orderId)
	{
		var order = _orderRepository.Orders.FirstOrDefault(o => o.Id == orderId);

		if(order != null)
		{
			order.Shipped = true;
			_orderRepository.SaveOrder(order);
		}

		return RedirectToAction(nameof(List));
	}

	public ViewResult Completed()
	{
		_cart.Clear();
		return View();
	}
}
