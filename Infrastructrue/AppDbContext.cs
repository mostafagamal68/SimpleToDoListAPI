using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructrue;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options), IUnitOfWork
{
    public DbSet<TodoItem> TodoItems { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var entity = modelBuilder.Entity<TodoItem>();
        entity.HasIndex(c => new { c.Id, c.Title }).IsUnique();
        entity.Property(c => c.Title).IsRequired();
        entity.HasIndex(c => c.Title);

        base.OnModelCreating(modelBuilder);
    }
}
