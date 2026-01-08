using Microsoft.AspNetCore.Http;
using GreenTogetheR.Data;
using GreenTogetheR.Models;
using Microsoft.AspNetCore.Mvc;

namespace GreenTogetheR.Controllers
{
    public class RegisteredUserController : Controller
    {
        private readonly GreenTogetherContext _context;
        private readonly IConfiguration _configuration;
        public RegisteredUserController(GreenTogetherContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(RegisteredUser model)
        {
            var adminUsername = _configuration["AdminCredentials:UserName"];
            var adminPassword = _configuration["AdminCredentials:Password"];
            if (model.UserName == adminUsername && model.Password == adminPassword) 
            {
                return RedirectToAction("Index", "Admin");
            }

            // Compare username and password with database
            var user = _context.RegisteredUsers
                .FirstOrDefault(u => u.UserName == model.UserName && u.Password == model.Password);

            if (user != null)
            {
                // Store username in cookie
                Response.Cookies.Append("UserName", user.UserName);
                //store userID in cookies
                HttpContext.Session.SetInt32("UserId", user.UserId);
                return RedirectToAction("UserDashboard");
            }

            ViewBag.Message = "Invalid username or password!";
            return View(model);
        }

        //User Dashboard
        public IActionResult UserDashboard()
        {
            var username = Request.Cookies["UserName"];

            if (string.IsNullOrEmpty(username))
            {
                return RedirectToAction("Login");
            }

            var user = _context.RegisteredUsers.FirstOrDefault(u => u.UserName == username);
            if (user == null)
            {
                return RedirectToAction("Login");
            }

            return View(user);
        }

        // Logout
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            Response.Cookies.Delete("UserName");
            return RedirectToAction("Login");
        }
        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SignUp(RegisteredUser registeredUser) 
        {
           if (string.IsNullOrWhiteSpace(registeredUser.CityName))
            {
                ViewBag.Message = "Please enter a city name.";
                return View(registeredUser);
            }

            var city = _context.Cities.FirstOrDefault(c => c.CityName.ToLower() == registeredUser.CityName.ToLower());
            if (city == null)
            {
                ViewBag.Message = "City not found. Please enter a valid city name.";
                return View(registeredUser);
            }

            registeredUser.CityId = city.CityId;

            _context.RegisteredUsers.Add(registeredUser);
            _context.SaveChanges();

            TempData["SuccessMessage"] = "Account created successfully, Let's build cleaner world Together!";
            return RedirectToAction("Login");
        }

        // GET: View/Edit Profile
        [HttpGet]
        public IActionResult Profile()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login");
            }

            var user = _context.RegisteredUsers.FirstOrDefault(u => u.UserId == userId);
            if (user == null)
            {
                return RedirectToAction("Login");
            }

            return View(user);
        }

        //Save Profile Changes
        [HttpPost]
        public IActionResult Profile(RegisteredUser model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = _context.RegisteredUsers.FirstOrDefault(u => u.UserId == model.UserId);
            if (user == null)
            {
                return RedirectToAction("Login");
            }

            
            user.FullName = model.FullName;
            user.Email = model.Email;
            user.Address = model.Address;
            user.UserName = model.UserName;
            user.Password = model.Password;

            // update city if CityName is changed
            if (!string.IsNullOrWhiteSpace(model.CityName))
            {
                var city = _context.Cities.FirstOrDefault(c => c.CityName.ToLower() == model.CityName.ToLower());
                if (city != null)
                {
                    user.CityId = city.CityId;
                }
            }

            _context.SaveChanges();
            TempData["SuccessMessage"] = "Profile updated successfully!";
            return RedirectToAction("Profile");
        }
    }
}
      