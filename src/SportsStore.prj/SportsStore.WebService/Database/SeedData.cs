using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using SportsStore.WebService.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SportsStore.WebService.Database;

public static class SeedData
{
	public static void EnsurePopulated(IApplicationBuilder applicationBuilder)
	{
		var context = applicationBuilder.ApplicationServices.GetRequiredService<ApplicationDbContext>();

		if(context.Products.Any()) return;

		for(var i = 0; i < 89; i++)
		{
			var product = new Product()
			{
				Name = SportsData.GetRandomItems(),
				Description = "Just test data",
				Category = SportsData.GetRandomCategory(),
				Price = SportsData.GetRandomPrice()
			};

			context.Products.Add(product);
		}

		context.SaveChanges();
	}
}

public static class IdentitySeedData
{
	private const string _adminLogin = "Admin3";
	private const string _adminPassword = "Secret123!";

	public static async void EnsurePopulated(IApplicationBuilder app)
	{
		var userManager = app.ApplicationServices.GetRequiredService<UserManager<IdentityUser>>();

		var user = await userManager.FindByNameAsync(_adminLogin);

		if(user == null)
		{
			user = new IdentityUser(_adminLogin);
			var res = await userManager.CreateAsync(user, _adminPassword);
		}
	}
}