using BankOfBadDecisions.Services;
using Microsoft.AspNetCore.Mvc;

namespace BankOfBadDecisions.Controllers
{
    public class LeaderboardController : Controller
    {
        private readonly LeaderboardService _leaderboard;
        public LeaderboardController(LeaderboardService leaderboard) { _leaderboard = leaderboard; }

        public async Task<IActionResult> Index()
        {
            var data = await _leaderboard.GetHallOfShameAsync();
            return View(data);
        }
    }
}
