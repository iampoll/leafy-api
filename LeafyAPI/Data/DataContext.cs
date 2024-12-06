using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using LeafyAPI.Models;

namespace LeafyAPI.Data
{
    public class DataContext : IdentityDbContext<User>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) 
        {
        }

        public required DbSet<Wallet> Wallets { get; set; }
        public required DbSet<Transaction> Transactions { get; set; }
        public required DbSet<Levels> Levels { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .Property(u => u.isOnboarded)
                .HasDefaultValue(false);

            modelBuilder.Entity<Wallet>()
                .Property(w => w.Balance)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Transaction>()
                .Property(t => t.Amount)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Levels>()
                .Property(l => l.Level)
                .HasDefaultValue(1);

            modelBuilder.Entity<Levels>()
                .Property(l => l.ExperiencePoints)
                .HasDefaultValue(0);

            modelBuilder.Entity<Levels>()
                .Property(l => l.ExperienceThreshold)
                .HasDefaultValue(100);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Levels)
                .WithOne(l => l.User)
                .HasForeignKey<Levels>(l => l.UserId);
        }
    }
} 