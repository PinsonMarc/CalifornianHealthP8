using CalifornianHealthApp.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace CalifornianHealthApp.Models
{
    public class CHDBContext : DbContext
    {
        public CHDBContext(DbContextOptions<CHDBContext> options) : base(options)
        {
        }
        public DbSet<Appointment> appointments { get; set; }

        public DbSet<Consultant> consultants { get; set; }

        public DbSet<ConsultantCalendar> consultantCalendars { get; set; }

        public DbSet<Patient> patients { get; set; }

    }
}