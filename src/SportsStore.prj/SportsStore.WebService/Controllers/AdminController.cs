using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportsStore.WebService.Database;
using SportsStore.WebService.Models;
using System;
using System.Linq;
//TODO: включить валидацию данных на стороне клиента. Страница 323. Разобраться с библиотеками фронтэнда. 

namespace SportsStore.WebService.Controllers;

[Authorize]
public class AdminController : Controller
{
	private readonly IProductRepository _productRepository;

	public AdminController(IProductRepository productRepository)
	{
		_productRepository = productRepository ?? throw new System.ArgumentNullException(nameof(productRepository));
	}

	public ViewResult Index() => View(_productRepository.Products);

	public ViewResult Edit(int productId)
	{
		var product = _productRepository.Products.FirstOrDefault(p => p.Id == productId);

		return View(product);
	}

	[HttpPost]
	public IActionResult Edit(Product product)
	{
		if(!ModelState.IsValid)
		{
			return View(product);
		}

		_productRepository.SaveProduct(product);
		TempData["message"] = $"{product.Name} has been saved";
		return RedirectToAction("Index");
	}

	public ViewResult Create() => View("Edit", new Product());

	[HttpPost]
	public IActionResult Delete(int productId)
	{
		var product = _productRepository.DeleteProduct(productId);
		if(product != null)
		{
			TempData["message"] = $"{product.Name} was deleted";
		}

		return RedirectToAction("Index");
	}
}
