using Domain.Infra.Todo.Entity;
using Microsoft.EntityFrameworkCore;

namespace Core.Todo.Infra;

public class EFDemoDbContext : DbContext
{
    public virtual DbSet<TodoTask> TodoTasks { get; set; }
    public EFDemoDbContext() { }

    public EFDemoDbContext(DbContextOptions options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TodoTask>().ToTable("Task", "demo")
        .HasKey(e => e.Id);

        modelBuilder.Entity<TodoTask>()
        .Property(e => e.Active).HasDefaultValue();
    }

    public void FindAsync(object[] objects)
    {
        throw new NotImplementedException();
    }
}


public sealed class EFDemoDbReadContext : EFDemoDbContext
{
    public EFDemoDbReadContext(DbContextOptions<EFDemoDbReadContext> options) : base(options)
    {
        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
    }
}