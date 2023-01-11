using Domain.DTO;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

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
        public async Task<HttpResponseMessage> AssignAppointment(Appointment appointment)
        {
            StringContent appointmentJson = new(
                JsonConvert.SerializeObject(appointment),
                Encoding.UTF8,
                Application.Json);
            //Should we double check here before confirming the appointment?
            HttpResponseMessage response = await _httpClient.PostAsync(API.Booking.createAppointment, appointmentJson);

            return response;
        }

        [HttpPost]
        public async Task<string> GetConsultantAppointments(ConsultantDailyAppointmentsDTO dailyAppointments)
        {
            StringContent dailyAppointmentsJson = new(
                JsonConvert.SerializeObject(dailyAppointments),
                Encoding.UTF8,
                Application.Json);

            HttpResponseMessage response = await _httpClient.PostAsync(API.Booking.getConsultantAppointments, dailyAppointmentsJson);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }

            return null;
        }
    }
}