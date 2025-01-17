﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RoyalHotel.Models;

namespace RoyalHotel.Controllers
{
    public class GalleriesController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _webHostEnviroment;

        public GalleriesController(ModelContext context, IWebHostEnvironment webHostEnviroment)
        {
            _context = context;
            _webHostEnviroment = webHostEnviroment;
        }

        // GET: Galleries
        public async Task<IActionResult> Index()
        {
              return _context.Galleries != null ? 
                          View(await _context.Galleries.ToListAsync()) :
                          Problem("Entity set 'ModelContext.Galleries'  is null.");
        }

        // GET: Galleries/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null || _context.Galleries == null)
            {
                return NotFound();
            }

            var gallery = await _context.Galleries
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gallery == null)
            {
                return NotFound();
            }

            return View(gallery);
        }

        // GET: Galleries/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Galleries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,ImageFile")] Gallery gallery)
        {
            if (ModelState.IsValid)
            {
                if (gallery.ImageFile != null)
                {
                    //1- Get root path
                    string wwwRootPath = _webHostEnviroment.WebRootPath;
                    //2- Create filename
                    string fileName = Guid.NewGuid().ToString() + "_" + gallery.ImageFile.FileName;
                    string path = Path.Combine(wwwRootPath, "Images", fileName);
                    //3- Upload image to folder
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await gallery.ImageFile.CopyToAsync(fileStream);
                    }
                    gallery.ImageUrl = fileName;
                }
                _context.Add(gallery);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(gallery);
        }

        // GET: Galleries/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null || _context.Galleries == null)
            {
                return NotFound();
            }

            var gallery = await _context.Galleries.FindAsync(id);
            if (gallery == null)
            {
                return NotFound();
            }
            return View(gallery);
        }

        // POST: Galleries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Id,Title,Description,ImageUrl")] Gallery gallery)
        {
            if (id != gallery.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gallery);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GalleryExists(gallery.Id))
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
            return View(gallery);
        }

        // GET: Galleries/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null || _context.Galleries == null)
            {
                return NotFound();
            }

            var gallery = await _context.Galleries
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gallery == null)
            {
                return NotFound();
            }

            return View(gallery);
        }

        // POST: Galleries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.Galleries == null)
            {
                return Problem("Entity set 'ModelContext.Galleries'  is null.");
            }
            var gallery = await _context.Galleries.FindAsync(id);
            if (gallery != null)
            {
                _context.Galleries.Remove(gallery);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GalleryExists(decimal id)
        {
          return (_context.Galleries?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
