﻿using Domain.DTO;
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
        public async Task<HttpResponseMessage> AssignAppointment(AssignAppointmentDTO dto)
        {
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync<AssignAppointmentDTO>(API.Booking.createAppointment, dto);

            return response;
        }

        [HttpPost]
        public async Task<string> GetConsultantAppointments(ConsultantDailyAppointmentsDTO dailyAppointments)
        {
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync<ConsultantDailyAppointmentsDTO>(API.Booking.getConsultantAppointments, dailyAppointments);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }
    }
}