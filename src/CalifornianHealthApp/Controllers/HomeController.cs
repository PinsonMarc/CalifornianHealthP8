
using CalifornianHealthApp.Models.DTOs;
using CalifornianHealthApp.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CalifornianHealthApp.Controllers
{

    public class HomeController : Controller
    {
        private IBookingService _bookingService;
        protected ILogger<BookingController> _logger;

        public HomeController(IBookingService bookingService, ILogger<BookingController> logger)
        {
            _bookingService = bookingService;
            _logger = logger;
        }
        public async Task<ActionResult> Index()
        {
            ConsultantModelList conList = new();
            List<Consultant> cons = new();

            cons = await _bookingService.FetchConsultants();
            conList.ConsultantsList = new SelectList(cons, "Id", "FName");
            conList.Consultants = cons;
            return View(conList);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}