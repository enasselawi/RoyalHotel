using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using RoyalHotel.Models;
using System.Threading.Tasks;

namespace RoyalHotel.Controllers
{
    public class LoginAndRegController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public LoginAndRegController(ModelContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {

                var user = new User
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                  

                };


                _context.Users.Add(user);
                await _context.SaveChangesAsync();


                var userLogin = new Userlogin
                {
                    UserId = user.UserId,
                    Username = model.Username,
                    Password = model.Password
                };


                _context.Userlogins.Add(userLogin);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Bind("Username,Password")] Userlogin model)
        {
            if (ModelState.IsValid)
            {

                var userLogin = await _context.Userlogins
                    .FirstOrDefaultAsync(ul => ul.Username == model.Username);

                if (userLogin != null && userLogin.Password == model.Password)
                {
                    //  user based on UserId .......to use in cjeck roleid
                    var auth = await _context.Users
                        .FirstOrDefaultAsync(u => u.UserId == userLogin.UserId);

                    //using the Session store the user who login inthe session
                    if (auth != null)
                    {

                        if (auth.RoleId == 1)
                        {   //go to Admin controller and Action Index
                            //key and value
                            //the key is AdminID / the id is decimal and mk cast to int
                            HttpContext.Session.SetInt32("AdminID", (int)userLogin.LoginId);
                            HttpContext.Session.SetString("AdminName", userLogin.Username ?? string.Empty);
                            return RedirectToAction("Index", "Admin");
                        }
                        else if (auth.RoleId == 2)
                        {
                            //??string to set empty string if the username null 
                            HttpContext.Session.SetInt32("UserID", (int)userLogin.LoginId);
                            HttpContext.Session.SetString("UserName", userLogin.Username ?? string.Empty);
                            return RedirectToAction("Index", "Home");
                        }
                    }
                }


                //ModelState.AddModelError("", "Invalid username or password.");
            }
            return View(model);
        }


        public IActionResult Logout()
        {
            HttpContext.Session.Remove("UserID"); // Remove UserID from session
            HttpContext.Session.Remove("UserName"); // Optionally remove UserName
            return RedirectToAction("Index", "Home"); // Redirect to home page or any page you want
        }
        public IActionResult AddTestimonial()
        {
            // Return the view for adding a testimonial
            return View();
        }

    }
}
