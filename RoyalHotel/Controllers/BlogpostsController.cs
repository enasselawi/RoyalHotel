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
    public class BlogpostsController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _webHostEnviroment;


        public BlogpostsController(ModelContext context, IWebHostEnvironment webHostEnviroment)
        {
            _context = context;
            _webHostEnviroment = webHostEnviroment;
        }

        // GET: Blogposts
        public async Task<IActionResult> Index()
        {
              return _context.Blogposts != null ? 
                          View(await _context.Blogposts.ToListAsync()) :
                          Problem("Entity set 'ModelContext.Blogposts'  is null.");
        }

        // GET: Blogposts/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null || _context.Blogposts == null)
            {
                return NotFound();
            }

            var blogpost = await _context.Blogposts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (blogpost == null)
            {
                return NotFound();
            }

            return View(blogpost);
        }

        // GET: Blogposts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Blogposts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,PostDate,ImageFile")] Blogpost blogpost)
        {
            if (ModelState.IsValid)
            {
                if (blogpost.ImageFile != null)
                {
                    //1- Get root path
                    string wwwRootPath = _webHostEnviroment.WebRootPath;
                    //2- Create filename
                    string fileName = Guid.NewGuid().ToString() + "_" + blogpost.ImageFile.FileName;
                    string path = Path.Combine(wwwRootPath, "Images", fileName);
                    //3- Upload image to folder
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await blogpost.ImageFile.CopyToAsync(fileStream);
                    }
                    blogpost.ImageUrl = fileName;
                }
                _context.Add(blogpost);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(blogpost);
        }

        // GET: Blogposts/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null || _context.Blogposts == null)
            {
                return NotFound();
            }

            var blogpost = await _context.Blogposts.FindAsync(id);
            if (blogpost == null)
            {
                return NotFound();
            }
            return View(blogpost);
        }

        // POST: Blogposts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Id,Title,Description,PostDate,ImageUrl")] Blogpost blogpost)
        {
            if (id != blogpost.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(blogpost);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BlogpostExists(blogpost.Id))
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
            return View(blogpost);
        }

        // GET: Blogposts/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null || _context.Blogposts == null)
            {
                return NotFound();
            }

            var blogpost = await _context.Blogposts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (blogpost == null)
            {
                return NotFound();
            }

            return View(blogpost);
        }

        // POST: Blogposts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.Blogposts == null)
            {
                return Problem("Entity set 'ModelContext.Blogposts'  is null.");
            }
            var blogpost = await _context.Blogposts.FindAsync(id);
            if (blogpost != null)
            {
                _context.Blogposts.Remove(blogpost);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BlogpostExists(decimal id)
        {
          return (_context.Blogposts?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
