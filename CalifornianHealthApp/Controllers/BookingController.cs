using CalifornianHealthApp.Models.DTOs;
using CalifornianHealthApp.Services;
using Domain.DTO;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net;

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

        [HttpPost]
        public async Task<IActionResult> ConfirmAppointment([FromBody] int appointmentId)
        {
            AssignAppointmentDTO dto = new()
            {
                AppointmentID = appointmentId,
                patient = new Patient
                {
                    ID = 1,
                    FName = "defaultName"
                }
            };
            HttpResponseMessage response = await _bookingService.AssignAppointment(dto);
            if (response.IsSuccessStatusCode)
                return Ok();
            if (response.StatusCode == HttpStatusCode.Conflict) return Conflict("The appointment just got reserved by an other user");
            return BadRequest();
        }

        [HttpGet]
        public IActionResult ConfirmAppointment()
        {
            return View();
        }
    }

}