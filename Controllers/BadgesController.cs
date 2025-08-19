using BankOfBadDecisions.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BankOfBadDecisions.Controllers
{
    public class BadgesController : Controller
    {
        private readonly AppDbContext _db;
        public BadgesController(AppDbContext db) { _db = db; }

        public async Task<IActionResult> Index()
        {
            var user = _db.Users.First();
            var badges = await _db.Badges.Where(b => b.UserId == user.Id)
                .OrderByDescending(b => b.EarnedAt).ToListAsync();
            return View(badges);
        }
    }
}
