using System.Collections.Generic;
using System.Linq;

namespace SportsStore.WebService.Models;

public class Cart
{
	private readonly List<CartLine> _cartLine = new();

	public virtual void AddItem(Product product, int quantity)
	{
		var line = _cartLine.Where(l => l.Product.Id == product.Id).FirstOrDefault();

		if(line == null)
		{
			var cartLine = new CartLine()
			{
				Product  = product,
				Quantity = quantity
			};

			_cartLine.Add(cartLine);
		}
		else
		{
			line.Quantity += quantity;
		}
	}

	public virtual void RemoveLine(Product product) => _cartLine.RemoveAll(l => l.Product.Id == product.Id);

	public virtual decimal ComputeTotalValue() => _cartLine.Sum(l => l.Quantity * l.Product.Price);

	public virtual void Clear() => _cartLine.Clear();

	public virtual IEnumerable<CartLine> Lines => _cartLine;
}
