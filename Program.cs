using System;
using BankingSystem.Controllers;
using BankingSystem.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BankingSystem
{
    class Program
    {
        static void Main(string[] args)
        {

            // Build the service provider
            var serviceProvider = BuildServiceProvider();

            // Create and configure the database (applies migrations)
            using (var scope = serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<BankingDbContext>();
                ApplyMigrations(dbContext);
            }

            // Start the application
            var appController = serviceProvider.GetRequiredService<AppController>();
            appController.Run();
        }

        private static IServiceProvider BuildServiceProvider()
        {
            var services = new ServiceCollection();

            // Add DbContext with connection string from appsettings.json
            services.AddDbContext<BankingDbContext>();

            // Register repository
            services.AddScoped<Repositories.IBankRepository, Repositories.BankRepository>();

            // Register commands and factory
            services.AddScoped<Services.CommandFactory>();
            services.AddScoped<Services.ICommand, Services.TransactionCommand>();
            services.AddScoped<Services.ICommand, Services.InterestRuleCommand>();
            services.AddScoped<Services.ICommand, Services.PrintStatementCommand>();

            // Register the main controller
            services.AddScoped<AppController>();

            return services.BuildServiceProvider();
        }

        private static void ApplyMigrations(BankingDbContext dbContext)
        {
            try
            {
                dbContext.Database.Migrate();
                Console.WriteLine("Database migrations applied successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error applying migrations: {ex.Message}");
            }
        }
    }
}
