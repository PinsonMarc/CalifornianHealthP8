using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{

    [Table("Appointment")]
    public partial class Appointment
    {
        public int Id { get; set; }

        public DateTime StartDateTime { get; set; }

        public DateTime EndDateTime { get; set; }

        public int? PatientId { get; set; }

        public int? ConsultantId { get; set; }

        public Consultant Consultant { get; set; }
    }
}
