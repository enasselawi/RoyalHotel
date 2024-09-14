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
    public class AbouthistoriesController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _webHostEnviroment;

        public AbouthistoriesController(ModelContext context, IWebHostEnvironment webHostEnviroment)
        {
            _context = context;
            _webHostEnviroment = webHostEnviroment;
        }

        // GET: Abouthistories
        public async Task<IActionResult> Index()
        {
              return _context.Abouthistories != null ? 
                          View(await _context.Abouthistories.ToListAsync()) :
                          Problem("Entity set 'ModelContext.Abouthistories'  is null.");
        }

        // GET: Abouthistories/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null || _context.Abouthistories == null)
            {
                return NotFound();
            }

            var abouthistory = await _context.Abouthistories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (abouthistory == null)
            {
                return NotFound();
            }

            return View(abouthistory);
        }

        // GET: Abouthistories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Abouthistories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,ImageFile")] Abouthistory abouthistory)
        {
            if (ModelState.IsValid)
            {

                if (abouthistory.ImageFile != null)
                {
                    //1- Get root path
                    string wwwRootPath = _webHostEnviroment.WebRootPath;
                    //2- Create filename
                    string fileName = Guid.NewGuid().ToString() + "_" + abouthistory.ImageFile.FileName;
                    string path = Path.Combine(wwwRootPath, "Images", fileName);
                    //3- Upload image to folder
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await abouthistory.ImageFile.CopyToAsync(fileStream);
                    }
                    abouthistory.ImageUrl = fileName;
                }
                _context.Add(abouthistory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(abouthistory);
        }

        // GET: Abouthistories/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null || _context.Abouthistories == null)
            {
                return NotFound();
            }

            var abouthistory = await _context.Abouthistories.FindAsync(id);
            if (abouthistory == null)
            {
                return NotFound();
            }
            return View(abouthistory);
        }

        // POST: Abouthistories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Id,Title,Description,ImageUrl")] Abouthistory abouthistory)
        {
            if (id != abouthistory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(abouthistory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AbouthistoryExists(abouthistory.Id))
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
            return View(abouthistory);
        }

        // GET: Abouthistories/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null || _context.Abouthistories == null)
            {
                return NotFound();
            }

            var abouthistory = await _context.Abouthistories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (abouthistory == null)
            {
                return NotFound();
            }

            return View(abouthistory);
        }

        // POST: Abouthistories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.Abouthistories == null)
            {
                return Problem("Entity set 'ModelContext.Abouthistories'  is null.");
            }
            var abouthistory = await _context.Abouthistories.FindAsync(id);
            if (abouthistory != null)
            {
                _context.Abouthistories.Remove(abouthistory);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AbouthistoryExists(decimal id)
        {
          return (_context.Abouthistories?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
