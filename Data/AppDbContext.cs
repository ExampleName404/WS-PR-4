using Microsoft.EntityFrameworkCore;
using TeamApi.Models;

namespace TeamApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Team> Teams { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Team>(e =>
        {
            e.Property(t => t.Name).IsRequired().HasMaxLength(100);
            e.Property(t => t.City).IsRequired().HasMaxLength(100);
            e.Property(t => t.Description).HasMaxLength(500);
        });
    }
}
