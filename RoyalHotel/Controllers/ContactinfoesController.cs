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
    public class ContactinfoesController : Controller
    {
        private readonly ModelContext _context;

        public ContactinfoesController(ModelContext context)
        {
            _context = context;
        }

        // GET: Contactinfoes
        public async Task<IActionResult> Index()
        {
              return _context.Contactinfos != null ? 
                          View(await _context.Contactinfos.ToListAsync()) :
                          Problem("Entity set 'ModelContext.Contactinfos'  is null.");
        }

        // GET: Contactinfoes/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null || _context.Contactinfos == null)
            {
                return NotFound();
            }

            var contactinfo = await _context.Contactinfos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contactinfo == null)
            {
                return NotFound();
            }

            return View(contactinfo);
        }

        // GET: Contactinfoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Contactinfoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Location,Phone,Email,OfficeHours")] Contactinfo contactinfo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contactinfo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(contactinfo);
        }

        // GET: Contactinfoes/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null || _context.Contactinfos == null)
            {
                return NotFound();
            }

            var contactinfo = await _context.Contactinfos.FindAsync(id);
            if (contactinfo == null)
            {
                return NotFound();
            }
            return View(contactinfo);
        }

        // POST: Contactinfoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Id,Location,Phone,Email,OfficeHours")] Contactinfo contactinfo)
        {
            if (id != contactinfo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contactinfo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContactinfoExists(contactinfo.Id))
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
            return View(contactinfo);
        }

        // GET: Contactinfoes/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null || _context.Contactinfos == null)
            {
                return NotFound();
            }

            var contactinfo = await _context.Contactinfos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contactinfo == null)
            {
                return NotFound();
            }

            return View(contactinfo);
        }

        // POST: Contactinfoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.Contactinfos == null)
            {
                return Problem("Entity set 'ModelContext.Contactinfos'  is null.");
            }
            var contactinfo = await _context.Contactinfos.FindAsync(id);
            if (contactinfo != null)
            {
                _context.Contactinfos.Remove(contactinfo);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContactinfoExists(decimal id)
        {
          return (_context.Contactinfos?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
