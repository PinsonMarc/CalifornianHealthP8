using CalifornianHealthApp.Models.DTOs;
using CalifornianHealthApp.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CalifornianHealthApp.Controllers
{
    public class BookingController : Controller
    {
        private IBookingService _bookingService;
        private ILogger<BookingController> _logger;

        public BookingController(IBookingService bookingService, ILogger<BookingController> logger)
        {
            _bookingService = bookingService;
            _logger = logger;
        }

        // GET: Booking
        //TODO: Change this method to display the consultant calendar. Ensure that the user can have a real time view of 
        //the consultant's availability;
        public async Task<ActionResult> GetConsultantCalendar()
        {
            ConsultantModelList conList = new();

            List<Consultant> cons = await _bookingService.FetchConsultants();
            conList.ConsultantsList = new SelectList(cons, "Id", "FName");
            conList.Consultants = cons;
            return View(conList);
        }

        //TODO: Change this method to ensure that members do not have to wait endlessly. 
        public async Task<ActionResult> ConfirmAppointment(Appointment model)
        {
            //Code to create appointment in database
            //This needs to be reassessed. Before confirming the appointment, should we check if the consultant calendar is still available?
            bool result = await _bookingService.CreateAppointment(model);

            return View(result);
        }
    }
}