using IdentityApi.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IdentityApi.Data;

public class ApiContext : IdentityDbContext
{
	public ApiContext (DbContextOptions options) : base(options)
	{

	}
	public DbSet<ApplicationUser> ApplicationUser { get; set; }
}