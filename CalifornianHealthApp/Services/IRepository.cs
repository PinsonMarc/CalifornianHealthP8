using CalifornianHealthApp.Models.Entities;

namespace CalifornianHealthApp.Services
{
    public interface IRepository
    {
        bool CreateAppointment(Appointment model);
        List<ConsultantCalendar> FetchConsultantCalendars();
        List<Consultant> FetchConsultants();
    }
}