using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using NUnit.Framework;
using SportsStore.WebService.Controllers;
using SportsStore.WebService.Database;
using SportsStore.WebService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsStore.WebService.Tests;
[TestFixture]
internal class AdminControllerTests
{
	private Mock<IProductRepository> _repositoryMock;
	private AdminController _controller;

	[SetUp]
	public void SetUp()
	{
		_repositoryMock = new Mock<IProductRepository>();
		_repositoryMock.Setup(r => r.Products).Returns(GetProducts());

		_controller = new AdminController(_repositoryMock.Object);
	}

	[Test]
	public void Index_Contains_All_Products()
	{
		//Act
		var products = GetViewModel<IEnumerable<Product>>(_controller.Index())?.ToArray();

		//Assert
		Assert.That(products.Count, Is.EqualTo(6));
		Assert.That(products[0].Name, Is.EqualTo("P1"));
		Assert.That(products[1].Name, Is.EqualTo("P2"));
		Assert.That(products[2].Name, Is.EqualTo("P3"));
	}

	[Test]
	public void Can_Edit_Product()
	{
		//Act
		var product1 = GetViewModel<Product>(_controller.Edit(1));
		var product2 = GetViewModel<Product>(_controller.Edit(2));
		var product3 = GetViewModel<Product>(_controller.Edit(3));

		//Assert
		Assert.That(product1.Id, Is.EqualTo(1));
		Assert.That(product2.Id, Is.EqualTo(2));
		Assert.That(product3.Id, Is.EqualTo(3));
	}

	[Test]
	public void Cannot_Edit_Nonexistent_Product()
	{
		//Act
		var product = GetViewModel<Product>(_controller.Edit(9));

		//Assert
		Assert.That(product, Is.Null);
	}

	[Test]
	public void Can_Save_Valid_Product()
	{
		//Arrange
		var tempData = new Mock<ITempDataDictionary>();
		_controller.TempData = tempData.Object;

		var product = new Product() { Name = "Test" };

		//Act
		var result = _controller.Edit(product);

		//Assert
		_repositoryMock.Verify(m => m.SaveProduct(product));
		Assert.That(result, Is.TypeOf(typeof(RedirectToActionResult)));
		Assert.That((result as RedirectToActionResult).ActionName, Is.EqualTo("Index"));
	}

	[Test]
	public void Cannot_Save_Invalid_Product()
	{
		//Arrange
		var tempData = new Mock<ITempDataDictionary>();
		_controller.TempData = tempData.Object;

		var product = new Product() { Name = "Test" };
		_controller.ModelState.AddModelError("error", "error");

		//Act
		var result = _controller.Edit(product);

		//Assert
		_repositoryMock.Verify(m => m.SaveProduct(product),Times.Never);
		Assert.That(result, Is.TypeOf(typeof(ViewResult)));
	}

	[Test]
	public void Can_Delete_Valid_Product()
	{
		//Act
		_controller.Delete(1);

		//Assert
		_repositoryMock.Verify(m => m.DeleteProduct(1));
	}

	private T GetViewModel<T>(IActionResult result) where T : class => (result as ViewResult)?.ViewData.Model as T;

	private IQueryable<Product> GetProducts()
	{
		return new List<Product>()
		{
			new Product(){Id=1, Name="P1", Category="Cat1"},
			new Product(){Id=2, Name="P2", Category="Cat1"},
			new Product(){Id=3, Name="P3", Category="Cat2"},
			new Product(){Id=4, Name="P4", Category="Cat1"},
			new Product(){Id=5, Name="P5", Category="Cat2"},
			new Product(){Id=6, Name="P6", Category="Cat3"},
		}.AsQueryable();
	}
}
