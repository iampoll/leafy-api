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
        public required DbSet<Level> Levels { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new WalletConfiguration());
            modelBuilder.ApplyConfiguration(new LevelsConfiguration());
            modelBuilder.ApplyConfiguration(new TransactionConfiguration());
        }
    }
} 