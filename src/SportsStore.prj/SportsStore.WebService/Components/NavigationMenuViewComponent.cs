using Microsoft.AspNetCore.Mvc;
using SportsStore.WebService.Database;
using System.Linq;

namespace SportsStore.WebService.Components;

public class NavigationMenuViewComponent : ViewComponent
{
	private readonly IProductRepository _productRepository;

	public NavigationMenuViewComponent(IProductRepository productRepository)
	{
		_productRepository = productRepository ?? throw new System.ArgumentNullException(nameof(productRepository));
	}

	public IViewComponentResult Invoke()
	{
		var products = _productRepository.Products.Select(x => x.Category).Distinct().OrderBy(x => x);
		ViewBag.SelectedCategory = RouteData?.Values["category"];
		return View(products);
	}

}
