using System.Reflection;
using Core.Entities;
using Core.Entities.Photos;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class FragDbContext : DbContext
{
    public FragDbContext(DbContextOptions<FragDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }


    public DbSet<Category>? Categories { get; set; }
    public DbSet<Publisher>? Publishers { get; set; }
    public DbSet<PhotoGame>? PhotoGames { get; set; }
    public DbSet<Game>? Games { get; set; }
}