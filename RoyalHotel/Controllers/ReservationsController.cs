using Microsoft.AspNetCore.Mvc;
using RoyalHotel.Models;

namespace RoyalHotel.Controllers
{
    public class ReservationsController : Controller
    {
        private readonly ModelContext _context;
        public ReservationsController (ModelContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        
        public IActionResult ReservationsChart()
        {
           //(Reservations وHotels)
            var data = from r in _context.Reservations
                       join h in _context.Hotels on r.HotelId equals h.HotelId
                       group r by h.Name into hotelGroup
                       select new ReservationsChartViewModel
                       {
                           HotelName = hotelGroup.Key,
                           ReservationCount = hotelGroup.Count()
                       };

            return View(data.ToList());
        }




































































    }
}
