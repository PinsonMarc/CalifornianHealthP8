using CalifornianHealthApp.Models.DTOs;
using CalifornianHealthApp.Services;
using Domain.DTO;
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
        public async Task<ActionResult> GetConsultantCalendar()
        {
            ConsultantModelList conList = new();

            List<Consultant> cons = await _bookingService.FetchConsultants();
            List<ConsultantCalendarDTO> calendarDTOs = await _bookingService.FetchConsultantCalendars();
            conList.ConsultantsList = new SelectList(cons, "Id", "FName");
            conList.Consultants = cons;
            return View(conList);
        }

        //TODO: Change this method to ensure that members do not have to wait endlessly. 
        public async Task<ActionResult> ConfirmAppointment(Appointment model)
        {
            //This needs to be reassessed. Before confirming the appointment, should we check if the consultant calendar is still available?
            bool result = await _bookingService.AssignAppointment(model);

            return View(result);
        }
    }
}