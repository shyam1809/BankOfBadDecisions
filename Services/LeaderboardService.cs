using BankOfBadDecisions.Data;
using Microsoft.EntityFrameworkCore;

namespace BankOfBadDecisions.Services
{
    public class LeaderboardService
    {
        private readonly AppDbContext _db;
        public LeaderboardService(AppDbContext db) { _db = db; }

        public async Task<List<(string Username, int Score, int Overdrafts, int Fees)>> GetHallOfShameAsync()
        {
            // Very simple formula: overdraft count + fees count + bad credit score
            var users = await _db.Users.Include(u => u.Transactions).ToListAsync();
            var rank = users.Select(u =>
            {
                var overdrafts = u.Transactions.Count(t => t.Amount < 0 && u.Balance < 0);
                var fees = u.Transactions.Count(t => t.IsFee);
                var score = u.BadCreditScore + overdrafts * 10 + fees * 5;
                return (u.Username, score, overdrafts, fees);
            })
            .OrderByDescending(x => x.score)
            .ToList();

            return rank;
        }
    }
}
