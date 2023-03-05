using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class FragDbContext : DbContext
{
    public FragDbContext(DbContextOptions options) : base(options)
    {
    }



    public DbSet<Category>? Categories { get; set; }
}