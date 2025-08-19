using BankOfBadDecisions.Models;
using Microsoft.EntityFrameworkCore;

namespace BankOfBadDecisions.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users => Set<User>();
        public DbSet<BankTransaction> Transactions => Set<BankTransaction>();
        public DbSet<Badge> Badges => Set<Badge>();
    }

    public static class Seed
    {
        public static void SeedIfEmpty(AppDbContext db)
        {
            if (!db.Users.Any())
            {
                var user = new User
                {
                    Username = "demo",
                    Balance = 5000m,
                    BadCreditScore = 100 // worse is better in this universe
                };
                db.Users.Add(user);
                db.SaveChanges();

                db.Transactions.AddRange(new[] {
                    new BankTransaction { UserId = user.Id, Amount = -299m, Category = "Coffee & Snacks", Description = "Caffeine binge", Date = DateTime.UtcNow.AddDays(-3) },
                    new BankTransaction { UserId = user.Id, Amount = -1499m, Category = "Impulse Buy", Description = "Shiny shoes", Date = DateTime.UtcNow.AddDays(-2) },
                    new BankTransaction { UserId = user.Id, Amount = -499m, Category = "Subscription", Description = "Forgotten gym", Date = DateTime.UtcNow.AddDays(-1) },
                });
                db.SaveChanges();
            }
        }
    }
}
