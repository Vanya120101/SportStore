using Microsoft.AspNetCore.Mvc;
using SportsStore.WebService.Database;

namespace SportsStore.WebService.Controllers;

public class ProductController : Controller
{
	private IProductRepository _productRepository;

	public ProductController(IProductRepository productRepository)
	{
		_productRepository = productRepository ?? throw new System.ArgumentNullException(nameof(productRepository));
	}

	public IActionResult List() => View(_productRepository.Products);
}
