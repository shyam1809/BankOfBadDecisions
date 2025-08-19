using BankOfBadDecisions.Data;
using Microsoft.AspNetCore.Mvc;

namespace BankOfBadDecisions.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _db;
        public HomeController(AppDbContext db) { _db = db; }

        public IActionResult Index()
        {
            var user = _db.Users.First();
            ViewBag.Username = user.Username;
            ViewBag.Balance = user.Balance;
            return View();
        }
    }
}
