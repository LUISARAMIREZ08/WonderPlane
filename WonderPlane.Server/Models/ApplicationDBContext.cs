using WonderPlane.Server.models;
using Microsoft.EntityFrameworkCore;

namespace WonderPlane.Server.Models;

public class ApplicationDBContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Forum> Forums { get; set; }
    public DbSet<Message> Messages { get; set; }

    public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        modelBuilder.Entity<User>()
            .Property(u => u.Role)
            .HasConversion<string>();

        modelBuilder.Entity<User>()
            .Property(u => u.Gender)
            .HasConversion<string>();

        base.OnModelCreating(modelBuilder);
    }
}