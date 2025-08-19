using BankOfBadDecisions.Data;
using BankOfBadDecisions.Models;
using Microsoft.EntityFrameworkCore;

namespace BankOfBadDecisions.Services
{
    public class BadgeService
    {
        private readonly AppDbContext _db;
        public BadgeService(AppDbContext db) { _db = db; }

        public async Task EvaluateAndAwardBadgesAsync(int userId)
        {
            var user = await _db.Users.Include(u => u.Transactions).Include(u => u.Badges).FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null) return;

            // 1) Master of Overdrafts
            if (user.Balance < 0 && !user.Badges.Any(b => b.Title == "Master of Overdrafts"))
                Award(user, "Master of Overdrafts", "You boldly explored the negative realm of money.");

            // 2) King of Late Fees
            var lateFees = user.Transactions.Count(t => t.IsFee);
            if (lateFees >= 3 && !user.Badges.Any(b => b.Title == "King of Late Fees"))
                Award(user, "King of Late Fees", "You paid more fines than taxes this month.");

            // 3) Debt Enthusiast
            var loans = user.Transactions.Count(t => t.IsLoan && t.Amount > 0);
            if (loans >= 3 && !user.Badges.Any(b => b.Title == "Debt Enthusiast"))
                Award(user, "Debt Enthusiast", "You collect loans like they are NFTs.");

            // 4) Certified Broke Genius
            var totalExpensesToday = user.Transactions.Where(t => t.Date.Date == DateTime.UtcNow.Date && t.Amount < 0).Sum(t => -t.Amount);
            if (totalExpensesToday >= 2000 && !user.Badges.Any(b => b.Title == "Certified Broke Genius"))
                Award(user, "Certified Broke Genius", "Legendary one-day spending spree achieved.");

            await _db.SaveChangesAsync();
        }

        private void Award(User user, string title, string description)
        {
            _db.Badges.Add(new Badge { UserId = user.Id, Title = title, Description = description });
        }
    }
}
