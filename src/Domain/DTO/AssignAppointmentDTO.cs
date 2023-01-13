using Domain.Entities;

namespace Domain.DTO
{
    public partial class AssignAppointmentDTO
    {
        public int AppointmentID { get; set; }

        public Patient Patient { get; set; }
    }
}
