using Infrastructure.SqlServer.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.SqlServer;

internal class AppDbContext : DbContext
{
    public DbSet<Example> Examples { get; init; }

    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}