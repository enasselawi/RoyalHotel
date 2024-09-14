using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RoyalHotel.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace RoyalHotel.Controllers
{
    public class AdminReportsController : Controller
    {
        private readonly ModelContext _context;
        public AdminReportsController(ModelContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(DateTime? startDate, DateTime? endDate)
        {
           
            var reservationsQuery = _context.Reservations
                .Include(r => r.User)
                .Include(r => r.Hotel)
                .AsQueryable(); 

          
            if (startDate.HasValue)
            {
                reservationsQuery = reservationsQuery.Where(r => r.ReservationDate >= startDate.Value);
            }

            if (endDate.HasValue)
            {
                reservationsQuery = reservationsQuery.Where(r => r.ReservationDate <= endDate.Value);
            }

            
            var reservations = await reservationsQuery.ToListAsync();

          
            ViewData["StartDate"] = startDate;
            ViewData["EndDate"] = endDate;

            return View(reservations);
        }

        
        [HttpPost]
        public IActionResult DownloadReport(string format, DateTime? startDate, DateTime? endDate)
        {
            var reservations = _context.Reservations
                .Include(r => r.User)
                .Include(r => r.Hotel)
                .Where(r => (startDate == null || r.ReservationDate >= startDate) &&
                            (endDate == null || r.ReservationDate <= endDate))
                .ToList();

           

            return RedirectToAction("Index");
        }


        //public async Task<IActionResult> Reports()
        //{
        //    // جلب بيانات الحجوزات
        //    var reservations = await _context.Reservations
        //        .Include(r => r.Hotel)
        //        .ToListAsync();

        //    // تجميع بيانات الرسوم البيانية
        //    var reservationData = reservations
        //        .GroupBy(r => r.ReservationDate.Value.ToString("yyyy-MM-dd"))
        //        .Select(g => new { Date = g.Key, Count = g.Count() })
        //        .ToList();

        //    // جلب بيانات الفنادق والغرف
        //    var hotelData = await _context.Hotels
        //        .Select(h => new { HotelName = h.Name, RoomCount = h.Rooms.Count() })
        //        .ToListAsync();

        //    // تمرير البيانات إلى العرض
        //    ViewData["ReservationData"] = reservationData;
        //    ViewData["HotelData"] = hotelData;

        //    return View();
        //}
    



}
}
