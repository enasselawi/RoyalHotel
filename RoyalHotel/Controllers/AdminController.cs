using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RoyalHotel.Models;
using RoyalHotel.Models.ViewModels;
using System.Linq;

namespace RoyalHotel.Controllers
{
    public class AdminController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _webHostEnviroment;

        public AdminController(ModelContext context, IWebHostEnvironment webHostEnviroment)
        {
            _context = context;
            _webHostEnviroment = webHostEnviroment;
        }

        public IActionResult Index()
        {
            // to Statics 
            ViewBag.Name = HttpContext.Session.GetString("AdminName");
            ViewBag.NumberOfRooms = _context.Rooms.Count();
            ViewBag.NumberOfHotels=_context.Hotels.Count();
            ViewBag.NumberOfUsers=_context.Users.Count();
            ViewBag.NumberOfLoginUser=_context.Userlogins.Count();
            ViewBag.TotalIncome=_context.Reservations.Sum(d=>d.TotalPrice);
            ViewBag.NumberOfReservations=_context.Reservations.Count();
            // Count the number of booked rooms
            int bookedRoomCount = _context.Rooms.Count(r => r.Booked == "Y");
            ViewBag.BookedRoomCount = bookedRoomCount;
            int NotbookedRoomCount = _context.Rooms.Count(r => r.Booked == "N");
            ViewBag.NotBookedRoomCount = bookedRoomCount;
            // to tables shown 
            //ViewData["Users"]=_context.Users.Count();
            //to use it in show the table 
            ViewData["Users"] = _context.Users.ToList();
            //امور ماليةةة
            var topHotel = _context.Reservations
           .GroupBy(r => r.HotelId)
           .Select(g => new
           {
               HotelName = _context.Hotels.FirstOrDefault(h => h.HotelId == g.Key).Name,
               ReservationCount = g.Count()
           })
           .OrderByDescending(x => x.ReservationCount)
           .FirstOrDefault();

            ViewBag.TopHotel = topHotel?.HotelName ?? "No data";
            ViewBag.TopHotelReservationCount = topHotel?.ReservationCount ?? 0;

            // حساب الأرباح الإجمالية
            var totalRevenue = _context.Reservations
                .Where(r => r.PaymentStatus == "Paid")
                .Sum(r => r.TotalPrice);

            ViewBag.TotalRevenue = totalRevenue;

            // الفندق الأقل حجزًا
            var leastBookedHotel = _context.Reservations
                .GroupBy(r => r.HotelId)
                .Select(g => new
                {
                    HotelName = _context.Hotels.FirstOrDefault(h => h.HotelId == g.Key).Name,
                    ReservationCount = g.Count()
                })
                .OrderBy(x => x.ReservationCount)
                .FirstOrDefault();

            ViewBag.LeastBookedHotel = leastBookedHotel?.HotelName ?? "No data";
            ViewBag.LeastBookedHotelReservationCount = leastBookedHotel?.ReservationCount ?? 0;

            // الفندق الأقل ربحا
            var leastProfitableHotel = _context.Reservations
                .Where(r => r.PaymentStatus == "Paid")
                .GroupBy(r => r.HotelId)
                .Select(g => new
                {
                    HotelName = _context.Hotels.FirstOrDefault(h => h.HotelId == g.Key).Name,
                    TotalRevenue = g.Sum(r => r.TotalPrice)
                })
                .OrderBy(x => x.TotalRevenue)
                .FirstOrDefault();

            ViewBag.LeastProfitableHotel = leastProfitableHotel?.HotelName ?? "No data";
            ViewBag.LeastProfitableHotelRevenue = leastProfitableHotel?.TotalRevenue ?? 0;

            // الفندق الأعلى ربحا
            var highestProfitableHotel = _context.Reservations
                .Where(r => r.PaymentStatus == "Paid")
                .GroupBy(r => r.HotelId)
                .Select(g => new
                {
                    HotelName = _context.Hotels.FirstOrDefault(h => h.HotelId == g.Key).Name,
                    TotalRevenue = g.Sum(r => r.TotalPrice)
                })
                .OrderByDescending(x => x.TotalRevenue)
                .FirstOrDefault();

            ViewBag.HighestProfitableHotel = highestProfitableHotel?.HotelName ?? "No data";
            ViewBag.HighestProfitableHotelRevenue = highestProfitableHotel?.TotalRevenue ?? 0;


           

            // نوع الغرفة الأعلى حجزًا
            var topRoomType = _context.Reservationdetails
                .GroupBy(rd => rd.Room.RoomType)
                .Select(g => new
                {
                    RoomType = g.Key,
                    RoomBookingCount = g.Count()
                })
                .OrderByDescending(x => x.RoomBookingCount)
                .FirstOrDefault();

            ViewBag.TopRoomType = topRoomType?.RoomType ?? "No data";
            ViewBag.TopRoomTypeCount = topRoomType?.RoomBookingCount ?? 0;

           












            var id = HttpContext.Session.GetInt32("AdminID");
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

        // GET: /Admin/EditProfile
        public IActionResult EditProfile()
        {
            var id = HttpContext.Session.GetInt32("AdminID");
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

        // POST: /Admin/EditProfile
        [HttpPost]
        public IActionResult EditProfile(EditProfileViewModel model)
        {
            if (ModelState.IsValid)
            {
                var id = HttpContext.Session.GetInt32("AdminID");
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

                        // توجيه إلى صفحة البروفايل بعد التحديث
                        return RedirectToAction("Profile");
                    }
                }
            }

            // إذا كانت هناك أخطاء في النموذج، أعد العرض مع النموذج
            return View(model);
        }

        // GET: /Admin/Profile
        public IActionResult Profile()
        {
            var id = HttpContext.Session.GetInt32("AdminID");
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

      public IActionResult Tables()
        {
            var viewModel = new DashboardViewModel
            {
                Roles = _context.Roles.ToList(),
                Users = _context.Users.ToList(),
                UserLogins = _context.Userlogins.ToList(),
                Hotels = _context.Hotels.ToList(),
                Rooms = _context.Rooms.ToList(),
                Reservations = _context.Reservations.ToList(),
                ReservationDetails = _context.Reservationdetails.ToList(),
                UserAccounts = _context.Useraccounts.ToList(),
                Contactformsubmissions =_context.Contactformsubmissions.ToList(),

            };
            return View(viewModel);
        }
       

      


    }
}
