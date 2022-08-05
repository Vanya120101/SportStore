using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.AspNetCore.Routing;
using Moq;
using NUnit.Framework;
using SportsStore.WebService.Components;
using SportsStore.WebService.Database;
using SportsStore.WebService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsStore.WebService.Tests;

[TestFixture]
internal class NavigationMenuComponentTests
{
	private NavigationMenuViewComponent _navigationMenu;

	[SetUp]
	public void SetUp()
	{
		var repositoryMock = new Mock<IProductRepository>();
		repositoryMock.Setup(m => m.Products).Returns(GetProducts());

		_navigationMenu = new NavigationMenuViewComponent(repositoryMock.Object);
	}

	[Test]
	public void Can_Select_Categories()
	{
		//Arrange
		var expectedCategories = new[] { "Apples", "Oranges", "Plums" };

		//Act
		var actualCategories = (_navigationMenu.Invoke() as ViewViewComponentResult).ViewData.Model as IEnumerable<string>;

		//Assert
		CollectionAssert.AreEqual(expectedCategories, actualCategories);
	}

	[Test]
	public void Indicates_Selected_Category()
	{
		//Arrange
		var selectedCategory = "Apples";

		_navigationMenu.ViewComponentContext = new ViewComponentContext
		{
			ViewContext = new ViewContext
			{
				RouteData = new RouteData()
			}
		};
		_navigationMenu.RouteData.Values["category"] = selectedCategory;

		//Act
		var actualCategory = (_navigationMenu.Invoke() as ViewViewComponentResult).ViewData["SelectedCategory"];

		//Assert
		Assert.That(actualCategory, Is.EqualTo(selectedCategory));
	}

	private IQueryable<Product> GetProducts()
	{
		return new List<Product>()
		{
			new Product(){Id=1, Name="P1", Category="Apples"},
			new Product(){Id=2, Name="P2", Category="Apples"},
			new Product(){Id=3, Name="P3", Category="Plums"},
			new Product(){Id=4, Name="P4", Category="Oranges"},
			new Product(){Id=5, Name="P5", Category="Plums"},
			new Product(){Id=6, Name="P6", Category="Apples"},
		}.AsQueryable();
	}
}
