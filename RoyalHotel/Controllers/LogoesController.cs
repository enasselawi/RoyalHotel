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
    public class LogoesController : Controller
    {
        private readonly ModelContext _context;

        public LogoesController(ModelContext context)
        {
            _context = context;
        }

        // GET: Logoes
        public async Task<IActionResult> Index()
        {
              return _context.Logos != null ? 
                          View(await _context.Logos.ToListAsync()) :
                          Problem("Entity set 'ModelContext.Logos'  is null.");
        }

        // GET: Logoes/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null || _context.Logos == null)
            {
                return NotFound();
            }

            var logo = await _context.Logos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (logo == null)
            {
                return NotFound();
            }

            return View(logo);
        }

        // GET: Logoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Logoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,LogoName")] Logo logo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(logo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(logo);
        }

        // GET: Logoes/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null || _context.Logos == null)
            {
                return NotFound();
            }

            var logo = await _context.Logos.FindAsync(id);
            if (logo == null)
            {
                return NotFound();
            }
            return View(logo);
        }

        // POST: Logoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Id,LogoName")] Logo logo)
        {
            if (id != logo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(logo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LogoExists(logo.Id))
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
            return View(logo);
        }

        // GET: Logoes/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null || _context.Logos == null)
            {
                return NotFound();
            }

            var logo = await _context.Logos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (logo == null)
            {
                return NotFound();
            }

            return View(logo);
        }

        // POST: Logoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.Logos == null)
            {
                return Problem("Entity set 'ModelContext.Logos'  is null.");
            }
            var logo = await _context.Logos.FindAsync(id);
            if (logo != null)
            {
                _context.Logos.Remove(logo);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LogoExists(decimal id)
        {
          return (_context.Logos?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
