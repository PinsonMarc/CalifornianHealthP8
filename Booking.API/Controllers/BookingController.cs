using Domain.DTO;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Booking.API.Controllers
{
    public class BookingController : Controller
    {
        protected IRepository _repo;
        private readonly ILogger<BookingController> _logger;

        public BookingController(IRepository repo, ILogger<BookingController> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return new RedirectResult("~/swagger");
        }

        [HttpGet]
        [Route("/Consultants")]
        public List<Consultant> GetConsultants()
        {
            List<Consultant> cons = _repo.FetchConsultants();
            return cons;
        }

        [HttpGet]
        [Route("/ConsultantCalendars")]
        public List<ConsultantCalendar> GetConsultantCalendars()
        {
            List<ConsultantCalendar> cons = _repo.FetchConsultantCalendars();
            return cons;
        }

        [HttpPost]
        [Route("/CreateAppointment")]
        public List<Consultant> CreateAppointment(Appointment model)
        {
            List<Consultant> cons = _repo.FetchConsultants();
            return cons;
        }
    }
}