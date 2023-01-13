using Booking.API;
using Booking.API.Controllers;
using Domain.DTO;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;

namespace Booking.tests
{
    public class IntegrationTests
    {
        private BookingController _bookingController;
        private Appointment appointmentExpected;
        public IntegrationTests()
        {
            //Arrange
            DbContextOptions<CHDBContext> options = new DbContextOptionsBuilder<CHDBContext>()
                .UseInMemoryDatabase(databaseName: "InMemoryTesting")
                .Options;

            Mock<ILogger<BookingController>> mockLoggerMock = new();

            CHDBContext context = new(options);

            appointmentExpected = new Appointment
            {
                Id = 12,
                ConsultantId = 1,
                StartDateTime = DateTime.Now
            };
            if (!context.consultants.Any(a => a.Id == 1))
            {
                context.Add(new Consultant
                {
                    Id = 1,
                    FName = "TestConsultant"
                });
            }

            if (!context.appointments.Any(a => a.Id == appointmentExpected.Id))
                context.appointments.Add(appointmentExpected);
            else context.appointments.First().PatientId = null;
            context.SaveChanges();
            _bookingController = new BookingController(context, mockLoggerMock.Object);
        }

        [Fact]
        public void Test_GetConsultantCalendars_ReturnListOfCalendar()
        {
            //act
            List<Consultant> res = _bookingController.GetConsultants();

            //assert
            Assert.NotEmpty(res);
        }

        [Fact]
        public void Test_GetConsultants_ReturnNotEmpty()
        {
            //act
            List<ConsultantCalendarDTO> res = _bookingController.GetConsultantCalendars();

            //assert
            Assert.NotNull(res);
            Assert.Single(res.Where(
                c => c.Consultant.Id == appointmentExpected.ConsultantId &&
                c.Dates.Exists(x => ((DateTime)x).Date == appointmentExpected.StartDateTime.Date)
            ));
        }

        [Fact]
        public void Test_ConsultantAppointments_FindExpected()
        {
            //act
            ConsultantDailyAppointmentsDTO model = new()
            {
                ConsultantId = (int)appointmentExpected.ConsultantId,
                Day = appointmentExpected.StartDateTime
            };
            List<Appointment> res = _bookingController.ConsultantAppointments(model);


            //assert
            Assert.Single(res);
        }


        [Fact]
        public async void Test_AssignAppointment_SuccessAndNotInCalendar()
        {
            //act
            AssignAppointmentDTO assignDto = new()
            {
                AppointmentID = appointmentExpected.Id,
                Patient = new Patient { ID = appointmentExpected.PatientId }
            };
            ConsultantDailyAppointmentsDTO dailyDto = new()
            {
                ConsultantId = (int)appointmentExpected.ConsultantId,
                Day = appointmentExpected.StartDateTime
            };


            IActionResult resAssign = await _bookingController.AssignAppointment(assignDto);
            Assert.IsType<OkResult>(resAssign);

            List<Appointment> resAppointment = _bookingController.ConsultantAppointments(dailyDto);

            Assert.Empty(resAppointment);
        }

        [Fact]
        public async void Test_DoubleAssignAppointment_ReturnConflict()
        {
            //act
            AssignAppointmentDTO assignDto = new()
            {
                AppointmentID = appointmentExpected.Id,
                Patient = new Patient { ID = appointmentExpected.PatientId }
            };

            //assert
            IActionResult resAssign = await _bookingController.AssignAppointment(assignDto);
            Assert.IsType<OkResult>(resAssign);
            resAssign = await _bookingController.AssignAppointment(assignDto);
            Assert.IsType<ConflictResult>(resAssign);
        }
    }
}