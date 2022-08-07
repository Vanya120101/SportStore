using Microsoft.AspNetCore.Mvc;
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
internal class OrderControllerTests
{
	private Mock<IOrderRepository> _repository;
	private Cart _cart;
	private Order _order;
	private OrderController _controlller;

	[SetUp]
	public void SetUp()
	{
		_repository = new Mock<IOrderRepository>();
		_cart = new Cart();
		_order = new Order();
		_controlller = new OrderController(_repository.Object, _cart);
	}

	[Test]
	public void Cannot_Checkout_Empty_Cart()
	{
		//Act
		var result = _controlller.Checkout(_order) as ViewResult;

		//Assert
		_repository.Verify(m => m.SaveOrder(_order), Times.Never);
		Assert.That(string.IsNullOrEmpty(result.ViewName), Is.True);
		Assert.That(result.ViewData.ModelState.IsValid, Is.False);
	}

	[Test]
	public void Cannot_Checkout_Invalid_ShippingDetails()
	{
		//Arrange
		_cart.AddItem(new Product(), 1);
		_controlller.ModelState.AddModelError("error", "error");

		//Act
		var result = _controlller.Checkout(_order) as ViewResult;

		//Assert
		_repository.Verify(m => m.SaveOrder(_order), Times.Never);
		Assert.That(string.IsNullOrEmpty(result.ViewName), Is.True);
		Assert.That(result.ViewData.ModelState.IsValid, Is.False);
	}

	[Test]
	public void Can_Checkout_And_Submit_Order()
	{
		//Arrange
		_cart.AddItem(new Product(), 1);

		//Act
		var result = _controlller.Checkout(_order) as RedirectToActionResult;

		//Assert
		_repository.Verify(m => m.SaveOrder(_order), Times.Once);
		Assert.That(result.ActionName, Is.EqualTo("Completed"));
	}
}
