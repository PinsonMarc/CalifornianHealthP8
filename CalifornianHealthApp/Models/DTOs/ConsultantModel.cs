using Domain.DTO;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CalifornianHealthApp.Models.DTOs
{
    public class ConsultantModel
    {
        public int id { get; set; }
        public string fname { get; set; }
        public string lname { get; set; }
        public string speciality { get; set; }
    }

    public class ConsultantModelList
    {
        public List<ConsultantCalendar> ConsultantCalendars { get; set; }
        public List<Consultant> Consultants { get; set; }
        public int SelectedConsultantId { get; set; }
        public SelectList ConsultantsList { get; set; }
    }

}