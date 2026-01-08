using GreenTogetheR.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GreenTogetheR.Controllers
{
    public class RecyclingCenterController : Controller
    {
        private readonly GreenTogetherContext _context;
        public RecyclingCenterController(GreenTogetherContext context)
        {
            _context = context;
        }

        public  IActionResult Index(int? cityId)
        {


            var cities = _context.Cities.ToList();
            ViewBag.Cities = cities;

            var query = _context.RecylingCenters
                     .Include(c => c.City)
                     .AsQueryable();

            if (cityId.HasValue) 
            {
                query= query.Where(c=>c.CityId == cityId.Value);
            }

            var centers = query.ToList();

            ViewBag.SelectedCityId = cityId;
                
            return View(centers);
        }

        
    }
}