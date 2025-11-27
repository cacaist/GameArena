using Microsoft.AspNetCore.Mvc;
using GameArena.Models;
using Microsoft.EntityFrameworkCore;

namespace GameArena.Controllers
{
    public class StoreController : Controller
    {
        private readonly GameArenaContext _context;

        public StoreController(GameArenaContext context)
        {
            _context = context;
        }

        public IActionResult Store()
        {
            SetUserSessionInfo();

            var rewards = _context.Rewards.ToList();
            return View("Store", rewards);
        }

        [HttpPost]
        public IActionResult Buy(int rewardId)
        {
            SetUserSessionInfo();

            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return Unauthorized();

            var user = _context.Users.FirstOrDefault(u => u.Id == userId);
            var reward = _context.Rewards.FirstOrDefault(r => r.Id == rewardId);

            if (user == null || reward == null) return NotFound();

            if (user.Score < reward.Cost)
            {
                TempData["Message"] = "Not enough points.";
                return RedirectToAction("Store");
            }

            user.Score -= reward.Cost;

            var userReward = new UserReward
            {
                UserId = user.Id,
                RewardId = reward.Id
            };

            _context.UserRewards.Add(userReward);
            _context.SaveChanges();

            TempData["Message"] = $"You purchased: {reward.Name}";
            return RedirectToAction("Store");
        }

       
        private void SetUserSessionInfo()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");

            if (userId != null)
            {
                var user = _context.Users.FirstOrDefault(u => u.Id == userId);
                ViewBag.UserLoggedIn = true;
                ViewBag.Username = user?.Username ?? "Unknown";
                ViewBag.Score = user?.Score ?? 0;
            }
            else
            {
                ViewBag.UserLoggedIn = false;
            }
        }
    }
}
