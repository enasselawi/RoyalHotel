using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RoyalHotel.Models;

namespace RoyalHotel.Controllers
{
    public class HoteltitlesController : Controller
    {
        private readonly ModelContext _context;

        public HoteltitlesController(ModelContext context)
        {
            _context = context;
        }

        // GET: Hoteltitles
        public async Task<IActionResult> Index()
        {
              return _context.Hoteltitles != null ? 
                          View(await _context.Hoteltitles.ToListAsync()) :
                          Problem("Entity set 'ModelContext.Hoteltitles'  is null.");
        }

        // GET: Hoteltitles/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null || _context.Hoteltitles == null)
            {
                return NotFound();
            }

            var hoteltitle = await _context.Hoteltitles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hoteltitle == null)
            {
                return NotFound();
            }

            return View(hoteltitle);
        }

        // GET: Hoteltitles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Hoteltitles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description")] Hoteltitle hoteltitle)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hoteltitle);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(hoteltitle);
        }

        // GET: Hoteltitles/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null || _context.Hoteltitles == null)
            {
                return NotFound();
            }

            var hoteltitle = await _context.Hoteltitles.FindAsync(id);
            if (hoteltitle == null)
            {
                return NotFound();
            }
            return View(hoteltitle);
        }

        // POST: Hoteltitles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Id,Title,Description")] Hoteltitle hoteltitle)
        {
            if (id != hoteltitle.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hoteltitle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HoteltitleExists(hoteltitle.Id))
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
            return View(hoteltitle);
        }

        // GET: Hoteltitles/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null || _context.Hoteltitles == null)
            {
                return NotFound();
            }

            var hoteltitle = await _context.Hoteltitles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hoteltitle == null)
            {
                return NotFound();
            }

            return View(hoteltitle);
        }

        // POST: Hoteltitles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.Hoteltitles == null)
            {
                return Problem("Entity set 'ModelContext.Hoteltitles'  is null.");
            }
            var hoteltitle = await _context.Hoteltitles.FindAsync(id);
            if (hoteltitle != null)
            {
                _context.Hoteltitles.Remove(hoteltitle);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HoteltitleExists(decimal id)
        {
          return (_context.Hoteltitles?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
