namespace CalifornianHealthApp.Models.Unused
{
    public class BookingModel
    {
        public PatientDetails Patient { get; set; }
        public ConsultantDetails Consultant { get; set; }
        public FacilityDetails Facility { get; set; }
        public PaymentDetails Payment { get; set; }
        public AppointmentDetails Appointment { get; set; }
    }

    public class AppointmentDetails
    {
        public Guid AppointmentId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public DateTime AppointmentTime { get; set; }
    }

    public class ConsultantDetails
    {
        public int ConsultantId { get; set; }
        public string ConsultantName { get; set; }
        public string ConsultantSpeciality { get; set; }
        public int FacilityId { get; set; }
    }

    public class FacilityDetails
    {
        public int FacilityId { get; set; }
        public string FacilityName { get; set; }
        public string FacilityAddressLine1 { get; set; }
        public string FacilityAddressLine2 { get; set; }
        public string FacilityRegion { get; set; }
        public string FacilityCity { get; set; }
        public string FacilityPostcode { get; set; }
        public string FacilityContactNumber { get; set; }
    }

    public class PatientDetails
    {
        public int patientId { get; set; }
        public string patientName { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string Region { get; set; }
        public string City { get; set; }
        public string Postcode { get; set; }
        public string contactNumber { get; set; }
    }

    public class PaymentDetails
    {
        public int PaymentId { get; set; }
        public double Payment { get; set; }
    }
}