using Microsoft.EntityFrameworkCore;
using SportsStore.WebService.Models;

namespace SportsStore.WebService.Database;

public class ApplicationDbContext : DbContext
{
	public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
	{
	}

	public DbSet<Product> Products { get; set; }
	public DbSet<Order> Orders { get; set; }
}
