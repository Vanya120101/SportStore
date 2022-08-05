using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using SportsStore.WebService.Controllers;
using SportsStore.WebService.Database;
using SportsStore.WebService.Models;
using SportsStore.WebService.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsStore.WebService.Tests;

[TestFixture]
internal class ProductControllerTests
{
	private ProductController _controller;

	[SetUp]
	public void SetUp()
	{
		var repositoryMock = new Mock<IProductRepository>();
		repositoryMock.Setup(m => m.Products).Returns(GetProducts());

		_controller = new ProductController(repositoryMock.Object) { PageSize = 3 };
	}

	[Test]
	public void Can_Send_Pagination_View_Model()
	{
		//Act
		var productList = _controller.List(null, 2).ViewData.Model as ProductsListViewModel;

		//Assert
		var actualPagingInfo = productList.PagingInfo;
		Assert.That(actualPagingInfo.CurrentPage, Is.EqualTo(2));
		Assert.That(actualPagingInfo.ItemsPerPage, Is.EqualTo(3));
		Assert.That(actualPagingInfo.TotalItems, Is.EqualTo(6));
		Assert.That(actualPagingInfo.TotalPages, Is.EqualTo(2));
	}

	[Test]
	public void Can_Paginate()
	{
		//Act
		var productList = _controller.List(null, 2).ViewData.Model as ProductsListViewModel;

		//Assert
		var actualProducts = productList.Products;
		var products = actualProducts.ToList();
		Assert.That(products.Count, Is.EqualTo(3));
		Assert.That(products[0].Name, Is.EqualTo("P4"));
		Assert.That(products[1].Name, Is.EqualTo("P5"));
		Assert.That(products[2].Name, Is.EqualTo("P6"));
	}

	[Test]
	public void Can_Filter_Products()
	{
		//Act
		var productList = _controller.List("Cat2", 1).ViewData.Model as ProductsListViewModel;

		//Assert
		var actualProducts = productList.Products;
		var products = actualProducts.ToList();
		Assert.That(products.Count, Is.EqualTo(2));
		Assert.That(products[0].Name, Is.EqualTo("P3"));
		Assert.That(products[1].Name, Is.EqualTo("P5"));
	}

	[Test]
	public void Generate_Category_Specific_Product_Count()
	{
		//Act
		Func<ViewResult, ProductsListViewModel> getModel = result => result?.ViewData?.Model as ProductsListViewModel;

		var res1   = getModel(_controller.List("Cat1"))?.PagingInfo.TotalItems;
		var res2   = getModel(_controller.List("Cat2"))?.PagingInfo.TotalItems;
		var res3   = getModel(_controller.List("Cat3"))?.PagingInfo.TotalItems;
		var resAll = getModel(_controller.List(null))?.PagingInfo.TotalItems;

		//Assert
		Assert.That(res1, Is.EqualTo(3));
		Assert.That(res2, Is.EqualTo(2));
		Assert.That(res3, Is.EqualTo(1));
		Assert.That(resAll, Is.EqualTo(6));
	}

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
