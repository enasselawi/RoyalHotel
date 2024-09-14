using Microsoft.AspNetCore.Mvc;
using RoyalHotel.Models;

namespace RoyalHotel.Controllers
{
    public class PaymentController : Controller
    {

        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _webHostEnviroment;
        
        public PaymentController(ModelContext context, IWebHostEnvironment webHostEnviroment)
        {
            _context = context;
            _webHostEnviroment = webHostEnviroment;
        }
        public IActionResult Index()
        {
            return View();
        }
















































    }
}
