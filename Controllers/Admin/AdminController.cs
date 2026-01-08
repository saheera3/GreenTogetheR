using Microsoft.AspNetCore.Mvc;

namespace GreenTogetheR.Controllers.Admin
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Logout()

        {
            HttpContext.Session.Clear();

            return RedirectToAction("Login", "RegisteredUser");
        }
    }
}
