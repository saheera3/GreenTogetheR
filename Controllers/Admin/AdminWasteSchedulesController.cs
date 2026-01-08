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
    public class AdminWasteSchedulesController : Controller
    {
        private readonly GreenTogetherContext _context;

        public AdminWasteSchedulesController(GreenTogetherContext context)
        {
            _context = context;
        }

        // GET: AdminWasteSchedules
        public async Task<IActionResult> Index()
        {
            var greenTogetherContext = _context.WasteSchedules.Include(w => w.City);
            return View(await greenTogetherContext.ToListAsync());
        }

        // GET: AdminWasteSchedules/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wasteSchedule = await _context.WasteSchedules
                .Include(w => w.City)
                .FirstOrDefaultAsync(m => m.ScheduleId == id);
            if (wasteSchedule == null)
            {
                return NotFound();
            }

            return View(wasteSchedule);
        }

        // GET: AdminWasteSchedules/Create
        public IActionResult Create()
        {
            ViewData["CityId"] = new SelectList(_context.Cities, "CityId", "CityId");
            return View();
        }

        // POST: AdminWasteSchedules/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ScheduleId,CityId,CollectionDay,WasteType")] WasteSchedule wasteSchedule)
        {
            if (ModelState.IsValid)
            {
                _context.Add(wasteSchedule);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CityId"] = new SelectList(_context.Cities, "CityId", "CityId", wasteSchedule.CityId);
            return View(wasteSchedule);
        }

        // GET: AdminWasteSchedules/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wasteSchedule = await _context.WasteSchedules.FindAsync(id);
            if (wasteSchedule == null)
            {
                return NotFound();
            }
            ViewData["CityId"] = new SelectList(_context.Cities, "CityId", "CityId", wasteSchedule.CityId);
            return View(wasteSchedule);
        }

        // POST: AdminWasteSchedules/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ScheduleId,CityId,CollectionDay,WasteType")] WasteSchedule wasteSchedule)
        {
            if (id != wasteSchedule.ScheduleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(wasteSchedule);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WasteScheduleExists(wasteSchedule.ScheduleId))
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
            ViewData["CityId"] = new SelectList(_context.Cities, "CityId", "CityId", wasteSchedule.CityId);
            return View(wasteSchedule);
        }

        // GET: AdminWasteSchedules/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wasteSchedule = await _context.WasteSchedules
                .Include(w => w.City)
                .FirstOrDefaultAsync(m => m.ScheduleId == id);
            if (wasteSchedule == null)
            {
                return NotFound();
            }

            return View(wasteSchedule);
        }

        // POST: AdminWasteSchedules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var wasteSchedule = await _context.WasteSchedules.FindAsync(id);
            if (wasteSchedule != null)
            {
                _context.WasteSchedules.Remove(wasteSchedule);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WasteScheduleExists(int id)
        {
            return _context.WasteSchedules.Any(e => e.ScheduleId == id);
        }
    }
}
