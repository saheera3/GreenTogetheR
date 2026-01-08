using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GreenTogetheR.Data;
using GreenTogetheR.Models;

namespace GreenTogetheR.Controllers.Admin
{
    [Route("Admin/[controller]/[action]")]
    public class AdminAwarenessArticlesController : Controller
    {
        private readonly GreenTogetherContext _context;

        public AdminAwarenessArticlesController(GreenTogetherContext context)
        {
            _context = context;
        }

        // GET: AdminAwarenessArticles
        public async Task<IActionResult> Index()
        {
            return View(await _context.AwarenessArticles.ToListAsync());
        }

        // GET: AdminAwarenessArticles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var awarenessArticles = await _context.AwarenessArticles
                .FirstOrDefaultAsync(m => m.ArticleId == id);
            if (awarenessArticles == null)
            {
                return NotFound();
            }

            return View(awarenessArticles);
        }

        // GET: AdminAwarenessArticles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AdminAwarenessArticles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ArticleId,ArticleTitle,ArticleDescription,FileUrl")] AwarenessArticles awarenessArticles)
        {
            if (ModelState.IsValid)
            {
                _context.Add(awarenessArticles);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(awarenessArticles);
        }

        // GET: AdminAwarenessArticles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var awarenessArticles = await _context.AwarenessArticles.FindAsync(id);
            if (awarenessArticles == null)
            {
                return NotFound();
            }
            return View(awarenessArticles);
        }

        // POST: AdminAwarenessArticles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ArticleId,ArticleTitle,ArticleDescription,FileUrl")] AwarenessArticles awarenessArticles)
        {
            if (id != awarenessArticles.ArticleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(awarenessArticles);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AwarenessArticlesExists(awarenessArticles.ArticleId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(awarenessArticles);
        }

        // GET: AdminAwarenessArticles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var awarenessArticles = await _context.AwarenessArticles
                .FirstOrDefaultAsync(m => m.ArticleId == id);
            if (awarenessArticles == null)
            {
                return NotFound();
            }

            return View(awarenessArticles);
        }

        // POST: AdminAwarenessArticles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var awarenessArticles = await _context.AwarenessArticles.FindAsync(id);
            if (awarenessArticles != null)
            {
                _context.AwarenessArticles.Remove(awarenessArticles);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AwarenessArticlesExists(int id)
        {
            return _context.AwarenessArticles.Any(e => e.ArticleId == id);
        }
    }
}
