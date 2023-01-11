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

            //List<Consultant> cons = await _bookingService.FetchConsultants();
            conList.consultantCalendar = await _bookingService.FetchConsultantCalendars();
            conList.ConsultantsList = new SelectList(conList.consultantCalendar, "Consultant.Id", "Consultant.FName");
            return View(conList);
        }

        [HttpPost]
        public async Task<IActionResult> ConsultantAppointments(ConsultantDailyAppointmentsDTO dto)
        {
            try
            {
                return Ok(await _bookingService.GetConsultantAppointments(dto));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error whule getting consultant appointments");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        //TODO: Change this method to ensure that members do not have to wait endlessly. 
        public async Task<IActionResult> ConfirmAppointment(Appointment model)
        {
            HttpResponseMessage response = await _bookingService.AssignAppointment(model);
            if (response.IsSuccessStatusCode)
                return View();
            return new StatusCodeResult((int)response.StatusCode);
        }
    }
}