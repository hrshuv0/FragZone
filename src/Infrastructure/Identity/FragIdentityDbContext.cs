using Core.Entities;
using Core.Entities.Identity;
using Core.Entities.Photos;
using Infrastructure.Data.Config;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Identity;

public class FragIdentityDbContext : IdentityDbContext<ApplicationUser>
{
    public FragIdentityDbContext(DbContextOptions<FragIdentityDbContext> options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new PhotoConfiguration());
        builder.ApplyConfiguration(new TeamConfiguration());
        
        base.OnModelCreating(builder);
    }


    public DbSet<ApplicationUser>? ApplicationUsers { get; set; }
    public DbSet<Photo>? Photos { get; set; }
    public DbSet<Team>? Teams { get; set; }

}