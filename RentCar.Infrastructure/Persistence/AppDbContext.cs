using Microsoft.EntityFrameworkCore;
using RentCar.Domain.Entities;

namespace RentCar.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Car> Cars { get; set; } = null!;
        public DbSet<Customer> Customers { get; set; } = null!;
        public DbSet<Rental> Rentals { get; set; } = null!;
        public DbSet<Payment> Payments { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Car>()
                .HasIndex(c => c.LicensePlate)
                .IsUnique();

            modelBuilder.Entity<Car>()
                .Property(c => c.DailyPrice)
                .HasColumnType("TEXT");

            modelBuilder.Entity<Rental>()
                .HasOne(r => r.Car)
                .WithMany(c => c.Rentals)
                .HasForeignKey(r => r.CarId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Rental>()
                .HasOne(r => r.Customer)
                .WithMany(c => c.Rentals)
                .HasForeignKey(r => r.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Rental)
                .WithOne(r => r.Payment)
                .HasForeignKey<Payment>(p => p.RentalId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}