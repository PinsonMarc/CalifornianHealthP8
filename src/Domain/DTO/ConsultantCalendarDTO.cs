using Domain.Entities;

namespace Domain.DTO
{
    public partial class ConsultantCalendarDTO
    {
        public Consultant Consultant { get; set; }

        public List<DateTime?> Dates { get; set; }
    }
}
