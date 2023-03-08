using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Identity;

public class FragIdentityDbContext : IdentityDbContext<ApplicationUser>
{
    public FragIdentityDbContext(DbContextOptions<FragIdentityDbContext> options) : base(options)
    {
    }


    public DbSet<ApplicationUser>? ApplicationUsers { get; set; }
}