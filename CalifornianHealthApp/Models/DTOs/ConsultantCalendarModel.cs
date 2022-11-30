namespace CalifornianHealthApp.Models.DTOs
{
    public class ConsultantCalendarModel
    {
        public int id { get; set; }
        public string consultantName { get; set; }

        public List<DateTime> availableDates { get; set; }
    }
}