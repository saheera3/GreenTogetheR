using GreenTogetheR.Data;
using GreenTogetheR.Models;
using Microsoft.AspNetCore.Mvc;

namespace GreenTogetheR.Controllers
{
    public class WasteScheduleController : Controller
    {
        private readonly GreenTogetherContext _context;
        public WasteScheduleController(GreenTogetherContext context)
        {
            _context = context;
        }

        //Reusable method to get logged-in user
        private RegisteredUser? GetLoggedInUser()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return null;

            return _context.RegisteredUsers.FirstOrDefault(u => u.UserId == userId);
        }

        public IActionResult Index()
        {
            var user = GetLoggedInUser();
            if (user == null)
                return RedirectToAction("Login");

            var schedule = _context.WasteSchedules
                                   .Where(s => s.CityId == user.CityId)
                                   .OrderBy(s => s.CollectionDay)
                                   .ToList();

            var todayDay = DateTime.Now.DayOfWeek.ToString();
            var todaySchedule = schedule.FirstOrDefault(s => s.CollectionDay.Equals(todayDay, StringComparison.OrdinalIgnoreCase));

            ViewBag.TodayDay = todayDay;
            ViewBag.TodayDate = DateTime.Now.ToString("MMMM dd yyyy");
            ViewBag.TodaySchedule = todaySchedule != null ? todaySchedule.WasteType : "No Waste collection today";

            var today = DateTime.Now.Date;
            ViewBag.HasRecycledToday = _context.PointEarneds
                                              .Where(p => p.UserId == user.UserId)
                                              .Any(p => p.ActionType == "Recycling" && p.DateEarned.Date == today);

            return View(schedule);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RecycleConfirm(IFormFile? Photo)
        {
            var user = GetLoggedInUser();
            if (user == null)
                return RedirectToAction("Login");

            var todayDay = DateTime.Now.DayOfWeek.ToString();
            var todaySchedule = _context.WasteSchedules.FirstOrDefault(s => s.CityId == user.CityId && s.CollectionDay == todayDay);

            if (todaySchedule == null)
            {
                TempData["RecycleMessage"] = "You can recycle on your city's collection day.";
                return RedirectToAction("Index");
            }

            if (Photo != null && Photo.Length > 0)
            {
                string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads/recycling");
                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(Photo.FileName);
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    Photo.CopyTo(stream);
                }
            }
            int newTotalPoints = (user.TotalPoints ?? 0) + 1;
            var point = new PointEarned
            {
                UserId = user.UserId,
                DateEarned = DateTime.Now,
                ActionType = "Recycling",
                PointsEarned = 1,
                TotalPoints = newTotalPoints
            };

            user.TotalPoints = newTotalPoints;
            _context.PointEarneds.Add(point);
            _context.SaveChanges();

            TempData["RecycleMessage"] = "Thank you for recycling! You earned 1 point 🌿";
            return RedirectToAction("Index");
        }
    }
}