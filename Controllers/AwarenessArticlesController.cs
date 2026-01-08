using Microsoft.AspNetCore.Mvc;
using GreenTogetheR.Models;
using GreenTogetheR.Data;

namespace GreenTogetheR.Controllers
{
    public class AwarenessArticlesController: Controller
    {
        private readonly GreenTogetherContext _context;
        public AwarenessArticlesController(GreenTogetherContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var articles = _context.AwarenessArticles.ToList();
            return View(articles);
        }
    }
}
