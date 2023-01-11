using Domain.DTO;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Booking.API.Controllers;

public class BookingController : Controller
{
    private readonly CHDBContext _context;
    private readonly ILogger<BookingController> _logger;

    public BookingController(CHDBContext context, ILogger<BookingController> logger)
    {
        _context = context;
        _logger = logger;
    }

    public IActionResult Index()
        => new RedirectResult("~/swagger");

    [HttpGet]
    [Route("/Consultants")]
    public List<Consultant> GetConsultants()
        => _context.consultants.ToList();

    [HttpGet]
    [Route("/ConsultantCalendars")]
    public List<ConsultantCalendarDTO> GetConsultantCalendars()
    {
        List<Appointment> appointments = _context.appointments
            .Where(a => a.PatientId == null)
            .Include(a => a.Consultant)
            .OrderBy(a => a.ConsultantId)
            .ThenBy(a => a.StartDateTime)
            .ToList();

        List<ConsultantCalendarDTO> res = new();

        int? currentConsultant = -1;
        DateTime currentDate = DateTime.MinValue;
        Appointment appointment;

        for (int i = 0; i < appointments.Count; i++)
        {
            appointment = appointments[i];
            if (currentConsultant != appointment.ConsultantId)
            {
                currentConsultant = appointment.ConsultantId;
                currentDate = DateTime.MinValue;
                res.Add(new ConsultantCalendarDTO
                {
                    Consultant = appointment.Consultant,
                    Dates = new List<DateTime?>()
                });
            }
            if (appointment.StartDateTime.Date != currentDate)
            {
                currentDate = appointment.StartDateTime.Date;
                res.Last().Dates.Add(currentDate);
            }
        }

        return res;
    }

    [HttpPost]
    [Route("/AssignAppointment")]
    public IActionResult AssignAppointment([FromBody] AssignAppointmentDTO model)
    {
        try
        {
            if (IsAppointmentFree(model.AppointmentID))
            {
                CreatePatient(model.patient);

                Appointment? appointment = _context.appointments.Single(a => a.Id == model.AppointmentID);
                appointment.PatientId = model.patient.ID;
                _context.SaveChanges();

                return Ok();
            }
            return Conflict();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "error while creating patient");
            return StatusCode(500);
        }
    }

    [HttpPost]
    [Route("/ConsultantAppointments")]
    public List<Appointment> ConsultantAppointments([FromBody] ConsultantDailyAppointmentsDTO model)
    {
        DateTime minDate = model.Day.Date;
        DateTime maxDate = minDate.AddDays(1);
        //Free appointment are non assigned appointment +> (patienId == null)
        List<Appointment> appointments = _context.appointments
            .Where(c => c.ConsultantId == model.ConsultantId && c.PatientId == null && c.StartDateTime >= minDate && c.StartDateTime <= maxDate)
            .OrderBy(a => a.StartDateTime).ToList();

        return appointments;
    }

    private void CreatePatient(Patient patient)
    {
        patient.ID = null;
        _context.patients.Add(patient);
        _context.SaveChanges();
    }

    private bool IsAppointmentFree(int appointmentId)
        => _context.appointments.Any(a => a.Id == appointmentId && a.PatientId == null);
}
