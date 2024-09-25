using WonderPlane.Server.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace WonderPlane.Server.Models;

public class ApplicationDbContext : IdentityDbContext<User>
{
    // public DbSet<User> Users { get; set; }
    public DbSet<Forum> Forums { get; set; }
    public DbSet<Message> Messages { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        modelBuilder.Entity<Forum>()
            .HasOne(f => f.User)
            .WithMany(u => u.Forums)
            .HasForeignKey(f => f.UserId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Message>()
            .HasOne(m => m.User)
            .WithMany(u => u.Messages)
            .HasForeignKey(m => m.UserId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Message>()
            .HasOne(m => m.Forum)
            .WithMany(f => f.Messages)
            .HasForeignKey(m => m.ForumId)
            .OnDelete(DeleteBehavior.NoAction);

        // modelBuilder.Entity<User>()
        //     .Property(u => u.Role)
        //     .HasConversion<string>();

        modelBuilder.Entity<User>()
            .Property(u => u.Gender)
            .HasConversion<string>();

        base.OnModelCreating(modelBuilder);

        modelBuilder.HasDefaultSchema("WonderPlane");
    }
}