using Microsoft.AspNetCore.Builder;
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
