using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RoyalHotel.Models;
using RoyalHotel.Models.ViewModels;
using System.Linq;
using System.Threading.Tasks;

namespace RoyalHotel.Controllers
{
    public class ReportsController : Controller
    {
        private readonly ModelContext _context;
        public ReportsController(ModelContext context) {

            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult MonthlyRevenueInput()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> MonthlyRevenueReport(MonthlyRevenueReportInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("MonthlyRevenueInput", model);
            }

            var totalRevenue = await _context.Reservations
     .Where(r => r.ReservationDate.Value.Month == model.Month && r.ReservationDate.Value.Year == model.Year)
     .Select(r => r.TotalPrice ?? 0)  
     .SumAsync();


            
            var report = new MonthlyRevenueReportViewModel
            {
                Month = model.Month,
                Year = model.Year,
                TotalRevenue = totalRevenue
            };

            return View(report);
        }


        [HttpGet]
        public IActionResult AnnualRevenueReport()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AnnualRevenueReport(int year)
        {
            
            var totalRevenue = _context.Reservations
                .Where(r => r.ReservationDate.HasValue && r.ReservationDate.Value.Year == year && r.PaymentStatus == "Paid")
                .Sum(r => r.TotalPrice ?? 0);  

            var model = new AnnualRevenueReportViewModel
            {
                Year = year,
                TotalRevenue = totalRevenue
            };

            return View("AnnualRevenueReportResult", model);
        }






















































    }
}
