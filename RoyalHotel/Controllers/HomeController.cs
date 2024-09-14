using Microsoft.AspNetCore.Mvc;
using RoyalHotel.Models;
using System.Diagnostics;

using RoyalHotel.Models.ViewModels;

namespace RoyalHotel.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ModelContext _context;

        public HomeController(ILogger<HomeController> logger, ModelContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            //about us
            var latestAboutHistory = _context.Abouthistories
               .OrderByDescending(x => x.Id)
               .FirstOrDefault();
            if (latestAboutHistory != null)
            {

                ViewBag.title = latestAboutHistory.Title;
                ViewBag.desc = latestAboutHistory.Description;
            }
            // under about us 
            var latesthome = _context.Homenavcs
               .OrderByDescending(x => x.Id)
               .FirstOrDefault();
            if (latesthome != null)
            {

                ViewBag.title1 = latesthome.Heading1;
                ViewBag.title2 = latesthome.Heading2;
                ViewBag.desc = latesthome.Description;
            }
            // logo 
            var logo =_context.Logos.OrderByDescending(x => x.Id).FirstOrDefault();
            if (logo != null)
            {

               ViewBag.logo = logo.LogoName;
                
            }
            //footers
            var footer=_context.Footers.OrderByDescending(x => x.Id).FirstOrDefault();
            if (footer != null)
            {
                ViewBag.phone = footer.SectionName;
                ViewBag.email = footer.Content;
            }


            //var homeNavCs = _context.Homenavcs.ToList();
            //var logos = _context.Logos.ToList();
            //var hotelTitles = _context.Hoteltitles.ToList();
            //var aboutHistories = _context.Abouthistories.ToList();
            //var blogPosts = _context.Blogposts.ToList();
            //var galleries = _context.Galleries.ToList();
            //var contactInfo = _context.Contactinfos.FirstOrDefault();
            //var footers = _context.Footers.ToList();
            //var services = _context.Services.ToList();
            //var Hotels = _context.Hotels.ToList();

            //var contentViewModel = new ContentViewModel
            //{
            //    HomeNavCs = homeNavCs,
            //    Logos = logos,
            //    HotelTitles = hotelTitles,
            //    AboutHistories = aboutHistories,
            //    BlogPosts = blogPosts,
            //    Galleries = galleries,
            //    ContactInfo = contactInfo,
            //    Footers = footers,
            //    Services = services,
            //    hotels = Hotels,

            //};


            //var desc =_context.Abouthistories.OrderByDescending(a=>a.Id).Select(a=>a.Description).FirstOrDefault();
            //ViewBag.desc = desc;
            return View(/*contentViewModel*/);

          
        }
        public IActionResult About()
        {

            // logo 
            var logo = _context.Logos.OrderByDescending(x => x.Id).FirstOrDefault();
            if (logo != null)
            {

                ViewBag.logo = logo.LogoName;

            }
            //footers
            var footer = _context.Footers.OrderByDescending(x => x.Id).FirstOrDefault();
            if (footer != null)
            {
                ViewBag.phone = footer.SectionName;
                ViewBag.email = footer.Content;
            }


            var latestAboutHistory = _context.Abouthistories
               .OrderByDescending(x => x.Id)
               .FirstOrDefault();
            if (latestAboutHistory != null)
            {
               
                ViewBag.title = latestAboutHistory.Title;
                ViewBag.desc = latestAboutHistory.Description;
            }
            return View();
        }
        public IActionResult Our_room()
        {
            // logo 
            var logo = _context.Logos.OrderByDescending(x => x.Id).FirstOrDefault();
            if (logo != null)
            {

                ViewBag.logo = logo.LogoName;

            }
            //footers
            var footer = _context.Footers.OrderByDescending(x => x.Id).FirstOrDefault();
            if (footer != null)
            {
                ViewBag.phone = footer.SectionName;
                ViewBag.email = footer.Content;
            }
            var hotels =_context.Hotels.ToList();
            return View(hotels);
        }
        public IActionResult GetRoomsByHotelId(int id)
        { // logo 
            var logo = _context.Logos.OrderByDescending(x => x.Id).FirstOrDefault();
            if (logo != null)
            {

                ViewBag.logo = logo.LogoName;

            }
            //footers
            var footer = _context.Footers.OrderByDescending(x => x.Id).FirstOrDefault();
            if (footer != null)
            {
                ViewBag.phone = footer.SectionName;
                ViewBag.email = footer.Content;
            }
            //var rooms = _context.Rooms.Where(x => x.HotelId == id).ToList();
            var rooms =_context.Rooms.Where(x=>x.HotelId == id).ToList();

            return View(rooms); 
            
        }
        public IActionResult Gallery()
        { // logo 
            var logo = _context.Logos.OrderByDescending(x => x.Id).FirstOrDefault();
            if (logo != null)
            {

                ViewBag.logo = logo.LogoName;

            }
            //footers
            var footer = _context.Footers.OrderByDescending(x => x.Id).FirstOrDefault();
            if (footer != null)
            {
                ViewBag.phone = footer.SectionName;
                ViewBag.email = footer.Content;
            }
            var Gallery =_context.Galleries.ToList();
            return View(Gallery);
        }
        public IActionResult Blog() {
            // logo 
            var logo = _context.Logos.OrderByDescending(x => x.Id).FirstOrDefault();
            if (logo != null)
            {

                ViewBag.logo = logo.LogoName;

            }
            //footers
            var footer = _context.Footers.OrderByDescending(x => x.Id).FirstOrDefault();
            if (footer != null)
            {
                ViewBag.phone = footer.SectionName;
                ViewBag.email = footer.Content;
            }
            var blogPosts = _context.Blogposts.ToList(); // Retrieve your blog posts from the database
            return View(blogPosts);
          

        }
        public IActionResult Search()
        { // logo 
            var logo = _context.Logos.OrderByDescending(x => x.Id).FirstOrDefault();
            if (logo != null)
            {

                ViewBag.logo = logo.LogoName;

            }
            //footers
            var footer = _context.Footers.OrderByDescending(x => x.Id).FirstOrDefault();
            if (footer != null)
            {
                ViewBag.phone = footer.SectionName;
                ViewBag.email = footer.Content;
            }
            return View();
        }
        public IActionResult Contact_Us() {
            // logo 
            var logo = _context.Logos.OrderByDescending(x => x.Id).FirstOrDefault();
            if (logo != null)
            {

                ViewBag.logo = logo.LogoName;

            }
            //footers
            var footer = _context.Footers.OrderByDescending(x => x.Id).FirstOrDefault();
            if (footer != null)
            {
                ViewBag.phone = footer.SectionName;
                ViewBag.email = footer.Content;
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ContactUs(Contactformsubmission submission)
        {
            if (ModelState.IsValid)
            {
                _context.Contactformsubmissions.Add(submission);
                await _context.SaveChangesAsync();
                return RedirectToAction("ContactUsConfirmation");
            }

            return View(submission); 
        }

        
        public IActionResult ContactUsConfirmation()
        {
            return View();
        }



        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
