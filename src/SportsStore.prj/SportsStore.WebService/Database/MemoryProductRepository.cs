using SportsStore.WebService.Models;
using System.Collections.Generic;
using System.Linq;

namespace SportsStore.WebService.Database;

public class FakeProductRepository : IProductRepository
{
	public IQueryable<Product> Products => new List<Product>
	{
		new Product{Name = "Phone", Price=255},
		new Product{Name = "Surf board", Price=2544},
		new Product{Name = "Running shoes", Price=95}
	}.AsQueryable();


}
