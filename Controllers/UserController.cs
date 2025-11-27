using GameArena.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GameArena.Controllers
{
    public class UserController : Controller
    {
        private readonly GameArenaContext _context;

        public UserController(GameArenaContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            SetUserSessionInfo();
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == username);

            if (user == null)
            {
                
                var newUser = new User
                {
                    Username = username,
                    Password = password,
                    Score = 100
                };

                _context.Users.Add(newUser);
                _context.SaveChanges();

                HttpContext.Session.SetInt32("UserId", newUser.Id);
                HttpContext.Session.SetString("Username", newUser.Username);
                return RedirectToAction("Index", "Home");
            }

            if (string.IsNullOrEmpty(user.Password))
            {
                user.Password = password;
                _context.SaveChanges();

                HttpContext.Session.SetInt32("UserId", user.Id);
                HttpContext.Session.SetString("Username", user.Username);
                return RedirectToAction("Index", "Home");
            }

            if (user.Password != password)
            {
                ViewBag.LoginError = "Incorrect Username or Password.";
                return View();
            }

            
            HttpContext.Session.SetInt32("UserId", user.Id);
            HttpContext.Session.SetString("Username", user.Username);
            return RedirectToAction("Index", "Home");
        }


        [HttpGet]
        public IActionResult CheckUserExists(string username)
        {
            bool exists = _context.Users.Any(u => u.Username == username);
            return Json(new { exists = exists });
        }

        public IActionResult Profile()
        {
            SetUserSessionInfo();

            int? userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
                return RedirectToAction("Login");

            var user = _context.Users.FirstOrDefault(u => u.Id == userId);

            var rewards = _context.UserRewards
                .Where(ur => ur.UserId == userId)
                .Include(ur => ur.Reward)
                .ToList();

            ViewBag.Rewards = rewards;

            return View(user);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
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
