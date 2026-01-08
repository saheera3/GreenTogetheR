using GreenTogetheR.Data;
using Microsoft.AspNetCore.Mvc;

namespace GreenTogetheR.Controllers
{
    
    public class PointEarnedController : Controller
    {
        private readonly GreenTogetherContext _context;
        public PointEarnedController(GreenTogetherContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login");
            }
            var user = _context.RegisteredUsers.FirstOrDefault(u=> u.UserId == userId);
            if (user == null) 
            {
                return NotFound();
            }
            var pointHistory = _context.PointEarneds
                                        .Where(p => p.UserId == userId)
                                        .OrderByDescending(p => p.DateEarned)
                                        .ToList();
            ViewBag.TotalPoints =user.TotalPoints;
            string newTitle;
            if (user.TotalPoints >= 100)
                newTitle = "Green Warrior";
            else if (user.TotalPoints >= 50)
                newTitle = "Eco Helper";
            else
                newTitle = "Member";

            //update database
            if (user.Title != newTitle)
            {
                user.Title = newTitle;
                _context.SaveChanges();
            }

            ViewBag.UserTilte= user.Title;
            return View(pointHistory);
        }
    }
}
