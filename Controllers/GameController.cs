using Microsoft.AspNetCore.Mvc;
using GameArena.Models;

namespace GameArena.Controllers
{
    public class GameController : Controller
    {
        private readonly GameArenaContext _context;

        public GameController(GameArenaContext context)
        {
            _context = context;
        }

        public IActionResult Blackjack()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");

            if (userId != null)
            {
                var user = _context.Users.FirstOrDefault(u => u.Id == userId);
                ViewBag.UserLoggedIn = true;
                ViewBag.Username = user.Username;
                ViewBag.Score = user.Score;
            }
            else
            {
                ViewBag.UserLoggedIn = false;
            }

            return View();
        }

        [HttpPost]
        public IActionResult AddScore([FromBody] ScoreRequest request)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            Console.WriteLine($"Session UserId: {userId}");

            Console.WriteLine($"Incoming Points: {request?.Points}");

            if (userId == null)
                return Unauthorized();

            var user = _context.Users.FirstOrDefault(u => u.Id == userId);
            if (user == null)
                return NotFound();

            user.Score += request.Points;
            Console.WriteLine($"New Score: {user.Score}");

            _context.SaveChanges();

            return Ok(new { success = true, newScore = user.Score });
        }

    }


    public class ScoreRequest
    {
        public int Points { get; set; }
    }
}
