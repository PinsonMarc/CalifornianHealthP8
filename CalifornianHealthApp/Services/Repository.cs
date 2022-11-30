using CalifornianHealthApp.Models;
using CalifornianHealthApp.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace CalifornianHealthApp.Services
{
    public class Repository : IRepository
    {
        private readonly CHDBContext _context;

        public Repository(CHDBContext dbContext)
        {
            _context = dbContext;
        }

        public List<Consultant> FetchConsultants()
        {
            var cons = _context.consultants.ToList();
            return cons;
        }

        public List<ConsultantCalendar> FetchConsultantCalendars()
        {
            //Should the consultant detail and the calendar (available dates) be clubbed together?
            //Is this the reason the calendar is slow to load? Rethink how we can rewrite this?

            return _context.consultantCalendars.ToList();
        }

        public bool CreateAppointment(Appointment model)
        {
            //Should we double check here before confirming the appointment?
            _context.appointments.Add(model);
            return true;
        }
    }
}