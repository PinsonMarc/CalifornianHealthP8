using Domain.DTO;
using Domain.Entities;

namespace CalifornianHealthApp.Services
{
    public interface IBookingService
    {
        Task<bool> CreateAppointment(Appointment model);
        Task<List<ConsultantCalendar>> FetchConsultantCalendars();
        Task<List<Consultant>> FetchConsultants();
    }
}