using Domain.DTO;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
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

        [HttpGet]
        public async Task<List<Consultant>> FetchConsultants()
        {
            string responseString = await _httpClient.GetStringAsync(API.Booking.getConsultants);

            List<Consultant> cons = JsonConvert.DeserializeObject<List<Consultant>>(responseString); ;
            return cons;
        }

        [HttpGet]
        public async Task<List<ConsultantCalendarDTO>> FetchConsultantCalendars()
        {
            string responseString = await _httpClient.GetStringAsync(API.Booking.getConsultantsCalendars);

            List<ConsultantCalendarDTO> calendars = JsonConvert.DeserializeObject<List<ConsultantCalendarDTO>>(responseString);
            return calendars;
        }

        [HttpPost]
        public async Task<bool> AssignAppointment(Appointment model)
        {
            //Should we double check here before confirming the appointment?
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync<Appointment>(API.Booking.createAppointment, model);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<bool>();
            }
            return false;
        }

        [HttpPost]
        public async Task<List<Appointment>> GetConsultantAppointments(ConsultantDailyAppointmentsDTO model)
        {
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync(API.Booking.createAppointment, model);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<Appointment>>();
            }

            return null;
        }
    }
}