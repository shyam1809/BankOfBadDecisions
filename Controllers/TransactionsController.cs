using BankOfBadDecisions.Data;
using BankOfBadDecisions.Models;
using BankOfBadDecisions.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BankOfBadDecisions.Controllers
{
    public class TransactionsController : Controller
    {
        private readonly AppDbContext _db;
        private readonly BadgeService _badges;
        public TransactionsController(AppDbContext db, BadgeService badges)
        {
            _db = db; _badges = badges;
        }

        public async Task<IActionResult> Index()
        {
            var tx = await _db.Transactions
                .OrderByDescending(t => t.Date)
                .Take(50)
                .ToListAsync();
            return View(tx);
        }

        [HttpPost]
        public async Task<IActionResult> Overspend()
        {
            var user = _db.Users.First();
            var rnd = Random.Shared.Next(200, 2500);
            var t = new BankTransaction
            {
                UserId = user.Id,
                Amount = -rnd,
                Category = "Impulse Buy",
                Description = "Retail therapy session",
                Date = DateTime.UtcNow
            };
            user.Balance -= rnd;
            _db.Transactions.Add(t);
            await _db.SaveChangesAsync();
            await _badges.EvaluateAndAwardBadgesAsync(user.Id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> PayLateFee()
        {
            var user = _db.Users.First();
            var fee = 350m;
            user.Balance -= fee;
            _db.Transactions.Add(new BankTransaction
            {
                UserId = user.Id,
                Amount = -fee,
                Category = "Late Fee",
                Description = "Reminder? Ignored.",
                Date = DateTime.UtcNow
            });
            await _db.SaveChangesAsync();
            await _badges.EvaluateAndAwardBadgesAsync(user.Id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> TakeLoan()
        {
            var user = _db.Users.First();
            var amount = Random.Shared.Next(1000, 5000);
            user.Balance += amount;
            _db.Transactions.Add(new BankTransaction
            {
                UserId = user.Id,
                Amount = amount,
                Category = "Loan",
                Description = "Totally safe loan",
                Date = DateTime.UtcNow
            });
            await _db.SaveChangesAsync();
            await _badges.EvaluateAndAwardBadgesAsync(user.Id);
            return RedirectToAction("Index");
        }
    }
}
