using Domain.DTO;
using Domain.Entities;
using Newtonsoft.Json;

namespace CalifornianHealthApp.Services
{
    public class BookingService : IBookingService
    {
        private readonly HttpClient _httpClient;

        public BookingService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Consultant>> FetchConsultants()
        {
            //    var uri = API.Catalog.GetAllCatalogItems(_remoteServiceBaseUrl,
            //                                         page, take, brand, type);

            string responseString = await _httpClient.GetStringAsync("/Consultants");

            List<Consultant> cons = JsonConvert.DeserializeObject<List<Consultant>>(responseString); ;
            return cons;
        }

        public async Task<List<ConsultantCalendar>> FetchConsultantCalendars()
        {
            //Should the consultant detail and the calendar (available dates) be clubbed together?
            //Is this the reason the calendar is slow to load? Rethink how we can rewrite this?
            throw new NotImplementedException();

            return null/*_context.consultantCalendars.ToList()*/;
        }

        public async Task<bool> CreateAppointment(Appointment model)
        {
            //Should we double check here before confirming the appointment?
            //_context.appointments.Add(model);
            ////await WaitCallback;
            throw new NotImplementedException();
            return true;
        }
    }
}