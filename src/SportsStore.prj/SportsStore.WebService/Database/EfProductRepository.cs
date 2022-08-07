using SportsStore.WebService.Models;
using System.Linq;

namespace SportsStore.WebService.Database;

public class EFProductRepository : IProductRepository
{
	private readonly ApplicationDbContext _context;

	public EFProductRepository(ApplicationDbContext context)
	{
		_context = context;
	}

	public IQueryable<Product> Products => _context.Products;

	public Product DeleteProduct(int productId)
	{
		var dbEntry = _context.Products.FirstOrDefault(p => p.Id == productId);
		if(dbEntry != null)
		{
			_context.Products.Remove(dbEntry);
			_context.SaveChanges();
		}

		return dbEntry;
	}

	public void SaveProduct(Product product)
	{
		if(product.Id == 0)
		{
			_context.Products.Add(product);
		}
		else
		{
			var dbEntry = _context.Products.FirstOrDefault(p => p.Id == product.Id);
			if(dbEntry != null)
			{
				dbEntry.Name = product.Name;
				dbEntry.Price = product.Price;
				dbEntry.Description = product.Description;
				dbEntry.Category = product.Category;
			}
		}

		_context.SaveChanges();
	}
}
