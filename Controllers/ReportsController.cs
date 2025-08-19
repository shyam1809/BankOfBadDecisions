using BankOfBadDecisions.Data;
using BankOfBadDecisions.Services;
using Microsoft.AspNetCore.Mvc;

namespace BankOfBadDecisions.Controllers
{
    public class ReportsController : Controller
    {
        private readonly RoastService _roast;
        private readonly AppDbContext _db;
        public ReportsController(RoastService roast, AppDbContext db) { _roast = roast; _db = db; }

        public async Task<IActionResult> Index()
        {
            var user = _db.Users.First();
            ViewBag.Roast = await _roast.GenerateMonthlyRoastAsync(user.Id);
            return View();
        }
    }
}
