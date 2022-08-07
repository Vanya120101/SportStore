using SportsStore.WebService.Models;
using System.Linq;

namespace SportsStore.WebService.Database;

public interface IProductRepository
{
	IQueryable<Product> Products { get; }

	void SaveProduct(Product product);

	Product DeleteProduct(int productId);
}
