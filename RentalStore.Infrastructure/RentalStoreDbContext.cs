using Microsoft.EntityFrameworkCore;
using RentalStore.Domain.Models;

namespace RentalStore.Infrastructure
{
    public class RentalStoreDbContext : DbContext
    {
        public DbSet<Equipment> Equipments { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<Rental> Rentals { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<RentalDetail> RentalDetails { get; set; }

        public RentalStoreDbContext(DbContextOptions<RentalStoreDbContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Equipment>()
            .HasOne(e => e.Category)
            .WithMany(c => c.Equipment)
            .HasForeignKey(e => e.CategoryId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
