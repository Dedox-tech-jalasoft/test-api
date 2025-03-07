using Microsoft.EntityFrameworkCore;
using WebApi.Infrastructure.Entities;

namespace WebApi.Infrastructure.Context
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Event> Events { get; set; }
        public DbSet<Entities.Application> Applications { get; set; }
        public DbSet<Entities.User> Users { get; set; }
        public DbSet<Entities.Booking> Bookings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(entity => entity.Events)
                .WithMany(entity => entity.Users)
                .UsingEntity<Booking>(
                    entity => entity.HasOne(entity => entity.Event).WithMany(entity => entity.Bookings),
                    entity => entity.HasOne(entity => entity.User).WithMany(entity => entity.Bookings));

            modelBuilder.Entity<Booking>()
                .HasIndex(entity => new { entity.EventId, entity.UserId})
                .IsUnique();
        }
    }
}
