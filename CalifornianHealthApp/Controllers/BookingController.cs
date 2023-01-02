using CalifornianHealthApp.Models.DTOs;
using CalifornianHealthApp.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CalifornianHealthApp.Controllers
{
    public class BookingController : Controller
    {
        protected IRepository _repo;
        protected ILogger<BookingController> _logger;

        public BookingController(IRepository repo, ILogger<BookingController> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        // GET: Booking
        //TODO: Change this method to display the consultant calendar. Ensure that the user can have a real time view of 
        //the consultant's availability;
        public ActionResult GetConsultantCalendar()
        {
            ConsultantModelList conList = new();

            List<Consultant> cons = _repo.FetchConsultants();
            conList.ConsultantsList = new SelectList(cons, "Id", "FName");
            conList.Consultants = cons;
            return View(conList);
        }

        //TODO: Change this method to ensure that members do not have to wait endlessly. 
        public ActionResult ConfirmAppointment(Appointment model)
        {
            //Code to create appointment in database
            //This needs to be reassessed. Before confirming the appointment, should we check if the consultant calendar is still available?
            bool result = _repo.CreateAppointment(model);

            return View();
        }
    }
}