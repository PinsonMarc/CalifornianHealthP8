using CalifornianHealthApp.Models.Entities;

namespace CalifornianHealthMonolithic.Code
{
    public interface IRepository
    {
        bool CreateAppointment(Appointment model);
        List<ConsultantCalendar> FetchConsultantCalendars();
        List<Consultant> FetchConsultants();
    }
}