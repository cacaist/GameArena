using System.Diagnostics;
using GameArena.Models;
using Microsoft.AspNetCore.Mvc;

namespace GameArena.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly GameArenaContext _context;

        public HomeController(ILogger<HomeController> logger, GameArenaContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult User()
        {
            SetUserSessionInfo();
            return View();
        }

        public IActionResult Privacy()
        {
            SetUserSessionInfo();
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            SetUserSessionInfo();
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Index()
        {
            // Veritabaný yerine manuel liste oluþturuyoruz
            var games = new List<Game>
            {
                // 1. OYUN (ÇALIÞAN)
                new Game
                {
                    Id = 1,
                    Name = "Blackjack",
                    Description = "Hit the 21",
                    ImageUrl = "/images/blackjack.jpg",
                    GameUrl = "/Game/Blackjack" //
                },

                // 2. OYUN (YOLDA)
                new Game
                {
                    Id = 2,
                    Name = "Space Wars",
                    Description = "Uzay savaþý çok yakýnda...",
                    ImageUrl = "/images/space.png",
                    GameUrl = "#"
                },

                // 3. OYUN (YOLDA)
                new Game
                {
                    Id = 3,
                    Name = "Racing Pro",
                    Description = "Pistlerin tozunu attýr!",
                    ImageUrl = "/images/racing.png",
                    GameUrl = "#" // Link yok
                }
            };

            return View(games);
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
