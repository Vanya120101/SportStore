using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using SportsStore.WebService.Infrastructure;
using System;

namespace SportsStore.WebService.Models;

public class SessionCart : Cart
{
	[JsonIgnore]
	public ISession Session { get; private set; }

	public static Cart GetCart(IServiceProvider services)
	{
		if(services is null) throw new ArgumentNullException(nameof(services));

		var session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
		var sessionCart = session?.GetJson<SessionCart>("Cart") ?? new SessionCart();

		sessionCart.Session = session;
		return sessionCart;
	}

	public override void AddItem(Product product, int quantity)
	{
		base.AddItem(product, quantity);
		Session.SetJson("Cart", this);
	}

	public override void RemoveLine(Product product)
	{
		base.RemoveLine(product);
		Session.SetJson("Cart", this);
	}

	public override void Clear()
	{
		base.Clear();
		Session.Remove("Cart");
	}
}
