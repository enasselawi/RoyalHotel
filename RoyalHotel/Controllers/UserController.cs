using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RoyalHotel.Models;
using RoyalHotel.Models.ViewModels;
using System.Linq;

namespace RoyalHotel.Controllers
{
    public class UserController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _webHostEnviroment;
        public UserController(ModelContext context, IWebHostEnvironment webHostEnviroment)
        {
            _context = context;
            _webHostEnviroment = webHostEnviroment;
        }
        public IActionResult Index()
        {


            ViewBag.Name = HttpContext.Session.GetString("UserName");

            var id = HttpContext.Session.GetInt32("UserID");
            if (id.HasValue)
            {
                var user = _context.Users.SingleOrDefault(x => x.UserId == id.Value);

                if (user != null)
                {
                    ViewBag.LastName = user.LastName;
                    ViewBag.FirstName = user.FirstName;
                    ViewBag.Email = user.Email;
                    ViewBag.Phone = user.Phone;
                }
                else
                {
                    return RedirectToAction("Login", "LoginAndReg");
                }
            }
            else
            {
                return RedirectToAction("Login", "LoginAndReg");
            }

            return View();
        }
        // GET: /User/EditProfile
        public IActionResult EditUserProfile()
        {
            var id = HttpContext.Session.GetInt32("UserID");
            if (id.HasValue)
            {
                var user = _context.Users.SingleOrDefault(x => x.UserId == id.Value);

                if (user != null)
                {
                    var model = new EditProfileViewModel
                    {
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Email = user.Email,
                        Phone = user.Phone
                    };

                    return View(model);
                }
                else
                {
                    return RedirectToAction("Login", "LoginAndReg");
                }
            }
            else
            {
                return RedirectToAction("Login", "LoginAndReg");
            }
        }

        // POST: /User/EditProfile
        [HttpPost]
        public IActionResult EditUserProfile(EditProfileViewModel model)
        {
            if (ModelState.IsValid)
            {
                var id = HttpContext.Session.GetInt32("UserID");
                if (id.HasValue)
                {
                    var user = _context.Users.SingleOrDefault(x => x.UserId == id.Value);

                    if (user != null)
                    {
                        user.FirstName = model.FirstName;
                        user.LastName = model.LastName;
                        user.Email = model.Email;
                        user.Phone = model.Phone;

                        _context.SaveChanges();

                       
                        return RedirectToAction("UserProfile");
                    }
                  
                }
            }


            return View(model);
        }
        // GET: /User/Profile
        public IActionResult UserProfile()
        {
            var id = HttpContext.Session.GetInt32("UserID");
            if (id.HasValue)
            {
                var user = _context.Users.SingleOrDefault(x => x.UserId == id.Value);

                if (user != null)
                {
                    var model = new EditProfileViewModel
                    {
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Email = user.Email,
                        Phone = user.Phone
                    };

                    // عرض المعلومات فقط دون التعديل
                    return View(model);
                }
                else
                {
                    return RedirectToAction("Login", "LoginAndReg");
                }
            }
            else
            {
                return RedirectToAction("Login", "LoginAndReg");
            }
        }

    }
} 