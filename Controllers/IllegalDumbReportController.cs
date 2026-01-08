using GreenTogetheR.Data;
using GreenTogetheR.Models;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace GreenTogetheR.Controllers
{

    public class IllegalDumbReportController : Controller
    {
        private readonly GreenTogetherContext _context;
        public IllegalDumbReportController(GreenTogetherContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(IllegalDumpReport model, IFormFile? Photo)
        {
            //get logged in user ID
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId != null) 
            { 
                model.UserId = userId.Value;
            }
            if (string.IsNullOrWhiteSpace(model.CityName))
            {
                ViewBag.Message = "Please enter a city name.";
                return View(model);
            }

            var city = _context.Cities.FirstOrDefault(c => c.CityName.ToLower() == model.CityName.ToLower());
            if (city == null)
            {
                ViewBag.Message = "City not found. Please enter a valid city name.";
                return View(model);
            }

            model.CityId = city.CityId;

            if (Photo != null && Photo.Length > 0)
            {
                string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(Photo.FileName);
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    Photo.CopyTo(stream);
                }

                model.PhotoUrl = "/uploads/" + uniqueFileName;
            }

            _context.IllegalDumpReports.Add(model);
            _context.SaveChanges();
            if (userId != null)
            {
                var pointRecord = new PointEarned
                {
                    UserId = userId.Value,
                    DateEarned = DateTime.Now,
                    ActionType = "Illegal Dumping Report",
                    PointsEarned = 1
                };
                _context.PointEarneds.Add(pointRecord);

                var user = _context.RegisteredUsers.FirstOrDefault(u => u.UserId == userId.Value);
                if (user != null)
                {
                    user.TotalPoints += 1;
                    _context.RegisteredUsers.Update(user);
                }
                _context.SaveChanges();
                ViewBag.Message = "Thank you for your eco friendly action! You have earned 1 point for reporting";
            }
            else 
            {
                ViewBag.Message = "Thank you for helping to make country clean! Your report has been submitted successfully.";
            }


            
            ModelState.Clear();
            return View();
        }
    }
}
