using BankOfBadDecisions.Data;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace BankOfBadDecisions.Services
{
    public class RoastService
    {
        private readonly AppDbContext _db;
        public RoastService(AppDbContext db) { _db = db; }

        public async Task<string> GenerateMonthlyRoastAsync(int userId)
        {
            var now = DateTime.UtcNow;
            var start = new DateTime(now.Year, now.Month, 1);
            var end = start.AddMonths(1);

            var tx = await _db.Transactions
                .Where(t => t.UserId == userId && t.Date >= start && t.Date < end)
                .ToListAsync();

            var totalExpense = tx.Where(t => t.Amount < 0).Sum(t => -t.Amount);
            var fees = tx.Where(t => t.IsFee).Sum(t => -t.Amount);
            var loans = tx.Where(t => t.IsLoan && t.Amount > 0).Sum(t => t.Amount);

            var sb = new StringBuilder();
            sb.AppendLine($"Report for {start:MMM yyyy}");
            sb.AppendLine($"• You spent {totalExpense:C} on things that spark short-term joy.");
            sb.AppendLine($"• You donated {fees:C} to your bank via fees. Philanthropy king.");
            if (loans > 0) sb.AppendLine($"• Loans taken: {loans:C}. Ah yes, free money (with a side of interest).");
            if (totalExpense > 3 * fees) sb.AppendLine("• At least your expenses dwarf your fees. Growth mindset!");
            if (fees > 0 && fees >= totalExpense * 0.3m) sb.AppendLine("• Fees are becoming your primary hobby.");
            if (tx.Count == 0) sb.AppendLine("• Nothing here. Suspiciously responsible?");

            return sb.ToString();
        }
    }
}
