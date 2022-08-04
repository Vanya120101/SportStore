using Microsoft.AspNetCore.Mvc;
using SportsStore.WebService.Database;
using SportsStore.WebService.Models.ViewModels;
using System.Linq;

namespace SportsStore.WebService.Controllers;

public class ProductController : Controller
{
	private readonly IProductRepository _productRepository;
	public int PageSize { get; init; } = 10;

	public ProductController(IProductRepository productRepository)
	{
		_productRepository = productRepository ?? throw new System.ArgumentNullException(nameof(productRepository));
	}

	public ViewResult List(string category, int productPage = 1)
	{
		var products = _productRepository.Products
										 .Where(x => x.Category == category || category == null)	
										 .OrderBy(p => p.Id)
										 .Skip((productPage - 1) * PageSize)
										 .Take(PageSize);

		var productsList = new ProductsListViewModel()
		{
			PagingInfo = new PagingInfo()
			{
				CurrentPage  = productPage,
				ItemsPerPage = PageSize,
				TotalItems   = _productRepository.Products.Count()
			},
			Products        = products,
			CurrentCaterogy = category
		};

		return View(productsList);
	}
}
