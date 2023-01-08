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
        public async Task<List<ConsultantCalendar>> FetchConsultantCalendars()
        {
            //Should the consultant detail and the calendar (available dates) be clubbed together?
            //Is this the reason the calendar is slow to load? Rethink how we can rewrite this?

            string responseString = await _httpClient.GetStringAsync(API.Booking.getConsultantsCalendars);

            List<ConsultantCalendar> calendars = JsonConvert.DeserializeObject<List<ConsultantCalendar>>(responseString);
            return calendars;
        }

        [HttpPost]
        public async Task<bool> CreateAppointment(Appointment model)
        {
            //Should we double check here before confirming the appointment?
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync<Appointment>(API.Booking.createAppointment, model);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<bool>();
            }
            return false;
        }
    }
}