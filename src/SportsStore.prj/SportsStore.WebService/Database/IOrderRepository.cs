using Microsoft.EntityFrameworkCore;
using SportsStore.WebService.Models;
using System.Linq;

namespace SportsStore.WebService.Database;

public interface IOrderRepository
{
	IQueryable<Order> Orders { get; }
	void SaveOrder(Order order);
}

public class OrderRepository : IOrderRepository
{
	private readonly ApplicationDbContext _applicationDbContext;

	public OrderRepository(ApplicationDbContext applicationDbContext)
	{
		_applicationDbContext = applicationDbContext;
	}

	public IQueryable<Order> Orders => _applicationDbContext.Orders
		.Include(o => o.Lines)
		.ThenInclude(l => l.Product);

	public void SaveOrder(Order order)
	{
		_applicationDbContext.AttachRange(order.Lines.Select(l => l.Product));
		if(order.Id == 0)
		{
			_applicationDbContext.Orders.Add(order);
		}
		_applicationDbContext.SaveChanges();
	}
}
