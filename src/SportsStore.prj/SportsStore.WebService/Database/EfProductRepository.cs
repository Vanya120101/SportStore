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
}
