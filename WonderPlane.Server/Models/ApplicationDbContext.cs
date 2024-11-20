using WonderPlane.Server.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace WonderPlane.Server.Models;

public class ApplicationDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Forum> Forums { get; set; }
    public DbSet<Message> Messages { get; set; }
    public DbSet<News> News { get; set; }
    public DbSet<Flight> Flights { get; set; }
    public DbSet<Seat> Seats { get; set; }
    public DbSet<Ticket> Tickets { get; set; }
    public DbSet<Purchase> Purchases { get; set; }
    public DbSet<BoardingPass> BoardingPasses { get; set; }
    public DbSet<Reservation> Reservations { get; set; }
    public DbSet<Traveler> Travelers { get; set; }
    public DbSet<Promotion> Promotions { get; set; }
    public DbSet<FlightRecommendation> FlightRecommendations { get; set; }
    public DbSet<Search> Searches { get; set; }
    public DbSet<Card> Cards { get; set; }
    public DbSet<Recommendation> Recommendations { get; set; }

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

        modelBuilder.Entity<User>()
            .Property(u => u.Role)
            .HasConversion<string>();

        modelBuilder.Entity<News>()
            .HasOne(n => n.Flight)
            .WithMany(f => f.News)
            .HasForeignKey(n => n.FlightId);

        modelBuilder.Entity<Seat>()
            .HasOne(s => s.Flight)
            .WithMany(f => f.Seats)
            .HasForeignKey(s => s.FlightId);

        modelBuilder.Entity<Seat>()
            .HasOne(s => s.Ticket)
            .WithOne(t => t.Seat)
            .HasForeignKey<Seat>(s => s.TicketId);

        modelBuilder.Entity<Ticket>()
            .HasOne(t => t.Seat)
            .WithOne(s => s.Ticket)
            .HasForeignKey<Ticket>(t => t.SeatId);

        modelBuilder.Entity<Ticket>()
            .HasOne(t => t.Purchase)
            .WithMany(p => p.Tickets)
            .HasForeignKey(t => t.PurchaseId);

        modelBuilder.Entity<Purchase>()
            .HasOne(p => p.RegisteredUser)
            .WithMany(ru => ru.Purchases)
            .HasForeignKey(p => p.RegisteredUserId);

        modelBuilder.Entity<BoardingPass>()
            .HasOne(bp => bp.Ticket)
            .WithOne(t => t.BoardingPass)
            .HasForeignKey<BoardingPass>(bp => bp.TicketId);

        modelBuilder.Entity<Ticket>()
            .HasOne(t => t.BoardingPass)
            .WithOne(bp => bp.Ticket)
            .HasForeignKey<Ticket>(t => t.BoardingPassId);

        modelBuilder.Entity<Reservation>()
            .HasOne(r => r.RegisteredUser)
            .WithMany(ru => ru.Reservations)
            .HasForeignKey(r => r.RegisteredUserId);

        modelBuilder.Entity<Ticket>()
            .HasOne(t => t.Reservation)
            .WithMany(r => r.Tickets)
            .HasForeignKey(t => t.ReservationId);

        modelBuilder.Entity<Traveler>()
            .HasOne(t => t.RegisteredUser)
            .WithOne(u => u.Traveler)
            .HasForeignKey<Traveler>(t => t.RegisteredUserId);

        modelBuilder.Entity<User>()
            .HasOne(u => u.Traveler)
            .WithOne(t => t.RegisteredUser)
            .HasForeignKey<User>(u => u.TravelerId);

        modelBuilder.Entity<Ticket>()
            .HasOne(t => t.Traveler)
            .WithMany(t => t.Tickets)
            .HasForeignKey(t => t.TravelerId);

        modelBuilder.Entity<Promotion>()
            .HasOne(p => p.Flight)
            .WithMany (f => f.Promotions)
            .HasForeignKey(p => p.FlightId);

        modelBuilder.Entity<FlightRecommendation>()
            .HasOne(fr => fr.Flight)
            .WithMany(f => f.FlightRecommendations)
            .HasForeignKey(fr => fr.FlightId);

        modelBuilder.Entity<FlightRecommendation>()
            .HasOne(fr => fr.Recommendation)
            .WithMany(r => r.FlightRecommendations)
            .HasForeignKey(fr => fr.RecommendationId);

        modelBuilder.Entity<Search>()
            .HasOne(s => s.RegisteredUser)
            .WithMany(ru => ru.Searches)
            .HasForeignKey(s => s.RegisteredUserId);

        modelBuilder.Entity<Card>()
            .HasOne(c => c.RegisteredUser)
            .WithMany(ru => ru.Cards)
            .HasForeignKey(c => c.RegisteredUserId);


        //precision for decimals

        //var decimalProps = modelBuilder.Model
        //.GetEntityTypes()
        //.SelectMany(t => t.GetProperties())
        //.Where(p => (System.Nullable.GetUnderlyingType(p.ClrType) ?? p.ClrType) == typeof(decimal));

        //foreach (var property in decimalProps)
        //{
        //    property.SetPrecision(18);
        //    property.SetScale(2);
        //}


        base.OnModelCreating(modelBuilder);

        modelBuilder.HasDefaultSchema("WonderPlane");
    }
}