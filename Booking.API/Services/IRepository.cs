using Domain.DTO;
using Domain.Entities;

namespace Booking.API
{
    public interface IRepository
    {
        bool CreateAppointment(Appointment model);
        List<ConsultantCalendar> FetchConsultantCalendars();
        List<Consultant> FetchConsultants();
    }
}