using BankingSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BankingSystem.Data
{
    public class BankingDbContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<InterestRule> InterestRules { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Load the configuration (assuming appsettings.json or environment variable)
            if (!optionsBuilder.IsConfigured)
            {
                var configuration = new ConfigurationBuilder()
                                    .SetBasePath(Directory.GetCurrentDirectory())
                                    .AddJsonFile("appsettings.json")
                                    .Build();


                // Get the connection string from the configuration
                var connectionString = configuration.GetConnectionString("BankingDbConnection");

                // Configure the DbContext to use SQL Server
                optionsBuilder.UseSqlServer(connectionString);
            }
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Transaction>()
                .HasKey(t => t.TransactionId);

            modelBuilder.Entity<InterestRule>()
                .HasKey(r => r.Date);

            modelBuilder.Entity<Account>()
                .HasKey(a => a.AccountId);
        }
    }
}
