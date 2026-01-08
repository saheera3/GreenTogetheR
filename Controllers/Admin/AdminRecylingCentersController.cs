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
    public class AdminRecylingCentersController : Controller
    {
        private readonly GreenTogetherContext _context;

        public AdminRecylingCentersController(GreenTogetherContext context)
        {
            _context = context;
        }

        // GET: AdminRecylingCenters
        public async Task<IActionResult> Index()
        {
            var greenTogetherContext = _context.RecylingCenters.Include(r => r.City);
            return View(await greenTogetherContext.ToListAsync());
        }

        // GET: AdminRecylingCenters/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recylingCenter = await _context.RecylingCenters
                .Include(r => r.City)
                .FirstOrDefaultAsync(m => m.CenterId == id);
            if (recylingCenter == null)
            {
                return NotFound();
            }

            return View(recylingCenter);
        }

        // GET: AdminRecylingCenters/Create
        public IActionResult Create()
        {
            ViewData["CityId"] = new SelectList(_context.Cities, "CityId", "CityId");
            return View();
        }

        // POST: AdminRecylingCenters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CenterId,CityId,CenterName,CenterAddress,ContactInfo")] RecylingCenter recylingCenter)
        {
            if (ModelState.IsValid)
            {
                _context.Add(recylingCenter);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CityId"] = new SelectList(_context.Cities, "CityId", "CityId", recylingCenter.CityId);
            return View(recylingCenter);
        }

        // GET: AdminRecylingCenters/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recylingCenter = await _context.RecylingCenters.FindAsync(id);
            if (recylingCenter == null)
            {
                return NotFound();
            }
            ViewData["CityId"] = new SelectList(_context.Cities, "CityId", "CityId", recylingCenter.CityId);
            return View(recylingCenter);
        }

        // POST: AdminRecylingCenters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CenterId,CityId,CenterName,CenterAddress,ContactInfo")] RecylingCenter recylingCenter)
        {
            if (id != recylingCenter.CenterId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(recylingCenter);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RecylingCenterExists(recylingCenter.CenterId))
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
            ViewData["CityId"] = new SelectList(_context.Cities, "CityId", "CityId", recylingCenter.CityId);
            return View(recylingCenter);
        }

        // GET: AdminRecylingCenters/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recylingCenter = await _context.RecylingCenters
                .Include(r => r.City)
                .FirstOrDefaultAsync(m => m.CenterId == id);
            if (recylingCenter == null)
            {
                return NotFound();
            }

            return View(recylingCenter);
        }

        // POST: AdminRecylingCenters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var recylingCenter = await _context.RecylingCenters.FindAsync(id);
            if (recylingCenter != null)
            {
                _context.RecylingCenters.Remove(recylingCenter);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RecylingCenterExists(int id)
        {
            return _context.RecylingCenters.Any(e => e.CenterId == id);
        }
    }
}
