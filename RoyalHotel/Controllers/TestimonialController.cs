using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RoyalHotel.Models;

namespace RoyalHotel.Controllers
{
    public class TestimonialController : Controller
    {

        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public TestimonialController(ModelContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            //var Testimonials = _context.Testimonials.Where(t =>t.IsApproved == "Y").ToList();
            return View(/*Testimonials*/);
        }
        [HttpGet]
        public IActionResult Testimonial()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Testimonial(Testimonials testimonial)
        {
            var userId = HttpContext.Session.GetInt32("UserID");
            if (userId == null)
            {
                return RedirectToAction("Login", "User");
            }

           
            testimonial.UserId = userId.Value;

            testimonial.CreatedAt = DateTime.Now;  // 
            testimonial.IsApproved = "N";
            if (ModelState.IsValid)
            {
                _context.Testimonials.Add(testimonial);
                await _context.SaveChangesAsync();
                return RedirectToAction("TestimonialConfirmation");
            }

            return View(testimonial);
        }


        public IActionResult TestimonialConfirmation()
        {
            return View();
        }

       
        public IActionResult PendingTestimonials()
        {
            var pendingTestimonials = _context.Testimonials
                .Where(t => t.IsApproved == "N")
                .ToList();
            return View(pendingTestimonials);
        }


        // الموافقة على شهادة
        [HttpPost]
        public async Task<IActionResult> ApproveTestimonial(decimal id)
        {
            var testimonial = await _context.Testimonials.FindAsync(id);
            if (testimonial == null)
            {
                return NotFound();
            }

            testimonial.IsApproved = "Y";
            _context.Testimonials.Update(testimonial);
            await _context.SaveChangesAsync();
            return RedirectToAction("PendingTestimonials");
        }

        // رفض شهادة
        [HttpPost]
        public async Task<IActionResult> RejectTestimonial(decimal id)
        {
            var testimonial = await _context.Testimonials.FindAsync(id);
            if (testimonial == null)
            {
                return NotFound();
            }

            _context.Testimonials.Remove(testimonial);
            await _context.SaveChangesAsync();
            return RedirectToAction("PendingTestimonials");
        }


        public IActionResult ApprovedTestimonials()
        {
            var testimonials = _context.Testimonials
       .Where(t => t.IsApproved == "Y")
       .Include(t => t.User)  
       .ToList();
            return View(testimonials);
        }
















































































    }
}
