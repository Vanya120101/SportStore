using SportsStore.WebService.Models;
using System.Linq;

namespace SportsStore.WebService.Database;

public class EFProductRepository : IProductRepository
{
	public IQueryable<Product> Products => throw new System.NotImplementedException();
}
