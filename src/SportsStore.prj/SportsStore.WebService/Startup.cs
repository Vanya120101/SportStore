using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SportsStore.WebService.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.WebService;

public class Startup
{
	private readonly IConfiguration _configuration;

	public Startup(IConfiguration configuration)
	{
		_configuration = configuration;
	}
	public void ConfigureServices(IServiceCollection services)
	{
		services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
			_configuration["Data:SportsStoreProducts:ConnectionString"]));
		services.AddTransient<IProductRepository, EFProductRepository>();
		services.AddMvc(options => options.EnableEndpointRouting = false);
	}

	public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
	{
		if(env.IsDevelopment())
		{
			app.UseDeveloperExceptionPage();
		}

		app.UseStatusCodePages();
		app.UseStaticFiles();
		app.UseMvc(routes =>
		{
			routes.MapRoute(
				name: null,
				template: "{category}/Page{productPage:int}",
				defaults: new { Controller = "Product", action = "List" });

			routes.MapRoute(
				name: null,
				template: "Page{productPage:int}",
				defaults: new { Controller = "Product", action = "List", productPage = 1 });

			routes.MapRoute(
				name: null,
				template: "{category}",
				defaults: new { Controller = "Product", action = "List", productPage = 1 });

			routes.MapRoute(
				name: null,
				template: "",
				defaults: new { Controller = "Product", action = "List", productPage = 1 });

			routes.MapRoute(
				name: null,
				template: "{controller=Product}/{action=List}/{id?}");
		});

		SeedData.EnsurePopulated(app);
	}
}
