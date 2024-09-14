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
    public class HomenavcsController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _webHostEnviroment;

        public HomenavcsController(ModelContext context, IWebHostEnvironment webHostEnviroment)
        {
            _context = context;
            _webHostEnviroment = webHostEnviroment;
        }

        // GET: Homenavcs
        public async Task<IActionResult> Index()
        {
              return _context.Homenavcs != null ? 
                          View(await _context.Homenavcs.ToListAsync()) :
                          Problem("Entity set 'ModelContext.Homenavcs'  is null.");
        }

        // GET: Homenavcs/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null || _context.Homenavcs == null)
            {
                return NotFound();
            }

            var homenavc = await _context.Homenavcs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (homenavc == null)
            {
                return NotFound();
            }

            return View(homenavc);
        }

        // GET: Homenavcs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Homenavcs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Heading1,Heading2,Description,ImageFile")] Homenavc homenavc)
        {
            if (ModelState.IsValid)
            {

                if (homenavc.ImageFile != null)
                {
                    //1- Get root path
                    string wwwRootPath = _webHostEnviroment.WebRootPath;
                    //2- Create filename
                    string fileName = Guid.NewGuid().ToString() + "_" + homenavc.ImageFile.FileName;
                    string path = Path.Combine(wwwRootPath, "Images", fileName);
                    //3- Upload image to folder
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await homenavc.ImageFile.CopyToAsync(fileStream);
                    }
                    homenavc.BackgroundImage = fileName;
                }
                _context.Add(homenavc);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(homenavc);
        }

        // GET: Homenavcs/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null || _context.Homenavcs == null)
            {
                return NotFound();
            }

            var homenavc = await _context.Homenavcs.FindAsync(id);
            if (homenavc == null)
            {
                return NotFound();
            }
            return View(homenavc);
        }

        // POST: Homenavcs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Id,Heading1,Heading2,Description,BackgroundImage")] Homenavc homenavc)
        {
            if (id != homenavc.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(homenavc);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HomenavcExists(homenavc.Id))
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
            return View(homenavc);
        }

        // GET: Homenavcs/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null || _context.Homenavcs == null)
            {
                return NotFound();
            }

            var homenavc = await _context.Homenavcs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (homenavc == null)
            {
                return NotFound();
            }

            return View(homenavc);
        }

        // POST: Homenavcs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.Homenavcs == null)
            {
                return Problem("Entity set 'ModelContext.Homenavcs'  is null.");
            }
            var homenavc = await _context.Homenavcs.FindAsync(id);
            if (homenavc != null)
            {
                _context.Homenavcs.Remove(homenavc);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HomenavcExists(decimal id)
        {
          return (_context.Homenavcs?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
