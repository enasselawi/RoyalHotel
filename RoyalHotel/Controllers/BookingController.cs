using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RoyalHotel.Models;
using RoyalHotel.Models.ViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace RoyalHotel.Controllers
{
    public class BookingController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IEmailSender _emailSender;

        public BookingController(ModelContext context, IWebHostEnvironment webHostEnvironment, IEmailSender emailSender)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _emailSender = emailSender;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Search(SearchViewModel model)
        {
            if (model.CheckInDate >= model.CheckOutDate)
            {
                ModelState.AddModelError("", "Check-in date must be before departure date.");
                return View(model);
            }

            var availableRooms = _context.Rooms
                .Include(r => r.Hotel)
                .Where(r => !_context.Reservationdetails
                    .Any(rd => rd.RoomId == r.RoomId && rd.CheckInDate <= model.CheckOutDate && rd.CheckOutDate >= model.CheckInDate)
                    || r.Booked == "N")
                .ToList();

            model.AvailableRooms = availableRooms ?? new List<Room>();
            return View(model);
        }

        [HttpPost]
        public IActionResult BookRoom(int roomId, DateTime checkInDate, DateTime checkOutDate)
        {
            if (HttpContext.Session.GetInt32("UserID") == null)
            {
                return RedirectToAction("Login", "LoginAndReg");
            }

            var room = _context.Rooms.FirstOrDefault(r => r.RoomId == roomId);

            if (room == null)
            {
                ModelState.AddModelError("", "Selected room does not exist.");
                return View("Search");
            }

            // Create PaymentViewModel for the user to proceed to payment
            var paymentViewModel = new PaymentViewModel
            {
                RoomId = roomId,
                RoomPrice = room.Price ?? 0m,
                CheckInDate = checkInDate,
                CheckOutDate = checkOutDate,
                TotalCost = (decimal)room.Price * (decimal)(checkOutDate - checkInDate).TotalDays
            };

            return View("Payment", paymentViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> ProcessPayment(PaymentViewModel model)
        {
            if (model.CheckInDate >= model.CheckOutDate)
            {
                ModelState.AddModelError("", "Check-in date must be before departure date.");
                return View("Payment", model);
            }

            if (!ModelState.IsValid)
            {
                ViewBag.ErrorMessage = "Please check your payment details.";
                return View("Payment", model);
            }

            var userId = HttpContext.Session.GetInt32("UserID");
            var userAccount = await _context.Useraccounts.FirstOrDefaultAsync(ua => ua.UserId == userId);
            var room = await _context.Rooms.FirstOrDefaultAsync(r => r.RoomId == model.RoomId);

            if (userAccount == null || room == null)
            {
                ModelState.AddModelError("", "The room or user account is invalid.");
                return View("Payment", model);
            }

            decimal totalCost = model.TotalCost;

            if (userAccount.Balance < totalCost)
            {
                ModelState.AddModelError("", "Insufficient balance .");
                return View("Payment", model);
            }

            if (userAccount.CardNumber != model.CardNumber ||
                userAccount.ExpiryDate != model.ExpiryDate ||
                userAccount.CVV != model.CVV)
            {
                ModelState.AddModelError("", "Incorrect card information.");
                return View("Payment", model);
            }

            // Process payment and update records
            userAccount.Balance -= totalCost;
            room.Booked = "Y";

            var reservation = new Reservation
            {
                UserId = userId.Value,
                HotelId = room.HotelId,
                TotalPrice = totalCost,
                PaymentStatus = "Paid",
                ReservationDate = DateTime.Now
            };

            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();

            var reservationDetail = new Reservationdetail
            {
                ReservationId = reservation.ReservationId,
                RoomId = model.RoomId,
                CheckInDate = model.CheckInDate,
                CheckOutDate = model.CheckOutDate,
                RoomPrice = model.RoomPrice
            };

            _context.Reservationdetails.Add(reservationDetail);
            await _context.SaveChangesAsync();

            return RedirectToAction("Invoice", new { reservationId = reservation.ReservationId });
        }



        public async Task<IActionResult> Invoice(int reservationId)
        {
            var reservation = _context.Reservations
                .Include(r => r.Reservationdetails)
                .ThenInclude(rd => rd.Room)
                .FirstOrDefault(r => r.ReservationId == reservationId);

            if (reservation == null)
            {
                return NotFound();
            }

            var invoiceViewModel = new InvoiceViewModel
            {
                ReservationId = reservation.ReservationId,
                ReservationDate = reservation.ReservationDate ?? DateTime.Now,
                TotalPrice = reservation.TotalPrice ?? 0,
                PaymentStatus = reservation.PaymentStatus
            };

            string emailContent = $@"
                <h1>Invoice</h1>
                <p>Booking number: {invoiceViewModel.ReservationId}</p>
                <p>Reservation date: {invoiceViewModel.ReservationDate.ToString("yyyy-MM-dd")}</p>
                <p>Total cost: {invoiceViewModel.TotalPrice.ToString("C")}</p>
                <p>Payment status: {invoiceViewModel.PaymentStatus}</p>";

            var user = _context.Users.FirstOrDefault(u => u.UserId == reservation.UserId);
            if (user != null)
            {
                try
                {
                    await _emailSender.SendEmailAsync(user.Email, "Your Invoice", emailContent);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to send email: {ex.Message}");
                    return StatusCode(500, $"Internal server error: {ex.Message}");
                }
            }

            return View(invoiceViewModel);
        }
    }
}
