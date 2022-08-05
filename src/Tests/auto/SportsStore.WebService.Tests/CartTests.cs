using NUnit.Framework;
using SportsStore.WebService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsStore.WebService.Tests;

[TestFixture]
internal class CartTests
{
    private Cart _cart;

    [SetUp]
    public void SetUp() => _cart = new Cart();

    [Test]
    public void Cat_Add_New_Lines()
    {
        //Assert
        var products = GetProducts();

        //Act
        _cart.AddItem(products[0], 1);
        _cart.AddItem(products[1], 1);

        var lines = _cart.Lines.ToArray();

        //Arrange
        Assert.That(lines.Count, Is.EqualTo(2));
        Assert.That(lines[0].Product.Id, Is.EqualTo(products[0].Id));
        Assert.That(lines[1].Product.Id, Is.EqualTo(products[1].Id));
    }

    [Test]
    public void Cart_Add_Quantity_For_Existing_Lines()
    {
        //Arrange
        var products = GetProducts();

        //Act
        _cart.AddItem(products[0], 1);
        _cart.AddItem(products[1], 1);
        _cart.AddItem(products[0], 10);

        var lines = _cart.Lines.ToArray();

        //Assert
        Assert.That(lines.Count, Is.EqualTo(2));
        Assert.That(lines[0].Quantity, Is.EqualTo(11));
        Assert.That(lines[1].Quantity, Is.EqualTo(1));
    }

    [Test]
    public void Can_Remove_Line()
    {
        //Arrange
        var products = GetProducts();

        _cart.AddItem(products[0], 1);
        _cart.AddItem(products[1], 2);
        _cart.AddItem(products[2], 4);
        _cart.AddItem(products[0], 2);

        //Act
        _cart.RemoveLine(products[0]);

        var lines = _cart.Lines.ToArray();

        //Assert
        Assert.That(lines.Count, Is.EqualTo(2));
        Assert.That(lines.Where(l=>l.Product.Id == products[0].Id).Count, Is.EqualTo(0));
    }

    [Test]
    public void Calculate_Cart_Total()
    {
        //Arrange
        var products = GetProducts();

        _cart.AddItem(products[0], 1);
        _cart.AddItem(products[1], 2);
        _cart.AddItem(products[2], 4);
        _cart.AddItem(products[0], 2);

        //Act
        var result = _cart.ComputeTotalValue();

        //Assert
        Assert.That(result, Is.EqualTo(100M * 3 + 10M * 2 + 15M * 4));
    }

    [Test]
    public void Can_Clear_Cart()
    {
        //Arrange
        var products = GetProducts();

        _cart.AddItem(products[0], 1);
        _cart.AddItem(products[1], 2);
        _cart.AddItem(products[2], 4);
        _cart.AddItem(products[0], 2);

        //Act
        _cart.Clear();

        var lines = _cart.Lines.ToArray();
        //Assert
        Assert.That(lines.Count, Is.EqualTo(0));
    }

    public IList<Product> GetProducts()
    {
        var products = new List<Product>
        {
            new Product(){Id = 1, Name = "P1", Price = 100M},
            new Product(){Id = 2, Name = "P2", Price = 10M},
            new Product(){Id = 3, Name = "P3", Price = 15M},
            new Product(){Id = 4, Name = "P4", Price = 3M},
            new Product(){Id = 5, Name = "P5", Price = 50M},
        };

        return products;
    }
}
