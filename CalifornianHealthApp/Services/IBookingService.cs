using Domain.DTO;
using Domain.Entities;

namespace CalifornianHealthApp.Services
{
    public interface IBookingService
    {
        Task<HttpResponseMessage> AssignAppointment(Appointment model);
        Task<List<ConsultantCalendarDTO>> FetchConsultantCalendars();
        Task<List<Consultant>> FetchConsultants();
        Task<string> GetConsultantAppointments(ConsultantDailyAppointmentsDTO model);
    }
}