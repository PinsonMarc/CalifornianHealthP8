using Domain.DTO;
using Domain.Entities;

namespace CalifornianHealthApp.Services
{
    public interface IBookingService
    {
        Task<bool> AssignAppointment(Appointment model);
        Task<List<ConsultantCalendarDTO>> FetchConsultantCalendars();
        Task<List<Consultant>> FetchConsultants();
        Task<List<Appointment>> GetConsultantAppointments(ConsultantDailyAppointmentsDTO model);
    }
}