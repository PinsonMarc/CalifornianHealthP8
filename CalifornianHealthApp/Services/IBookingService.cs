using Domain.DTO;
using Domain.Entities;

namespace CalifornianHealthApp.Services
{
    public interface IBookingService
    {
        Task<bool> CreateAppointment(Appointment model);
        Task<List<AssignAppointmentDTO>> FetchConsultantCalendars();
        Task<List<Consultant>> FetchConsultants();
    }
}