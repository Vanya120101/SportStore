using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SportsStore.WebService.Database;

public class AppIdentityDbContext : IdentityDbContext<IdentityUser>
{
	public AppIdentityDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
	{

	}
}
