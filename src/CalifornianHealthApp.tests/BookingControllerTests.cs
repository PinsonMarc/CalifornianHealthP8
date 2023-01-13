using CalifornianHealthApp.Controllers;
using CalifornianHealthApp.Services;
using Domain.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net;

namespace CalifornianHealthApp.tests
{
    public class BookingControllerTests
    {

        private Mock<IBookingService> mockBookingService;
        private Mock<ILogger<BookingController>> mockLogger;

        public BookingControllerTests()
        {
            //Arrange
            mockLogger = new();
            mockBookingService = new();

        }

        [Fact]
        public async Task Test_FetchConsultants_ReturnsOkObjectResult()
        {
            mockBookingService.Setup(s => s.FetchConsultantCalendars()).ReturnsAsync(() => new List<ConsultantCalendarDTO>());

            //act
            BookingController bookingController = new(mockBookingService.Object, mockLogger.Object);
            ActionResult res = await bookingController.GetConsultantCalendar();

            //assert
            Assert.IsType<OkObjectResult>(res);
        }


        [Fact]
        public async Task Test_GetConsultantAppointments_ReturnsOkObjectResult()
        {
            string expected = "expected";
            mockBookingService.Setup(s => s.GetConsultantAppointments(It.IsAny<ConsultantDailyAppointmentsDTO>())).ReturnsAsync(() => expected);

            //act
            BookingController bookingController = new(mockBookingService.Object, mockLogger.Object);
            IActionResult res = await bookingController.ConsultantAppointments(new ConsultantDailyAppointmentsDTO());

            // Assert
            OkObjectResult okResult = Assert.IsType<OkObjectResult>(res);
            Assert.Equal(expected, okResult.Value);
        }

        [Fact]
        public async Task Test_ConfirmAppointment_ReturnsOkObjectResult()
        {
            mockBookingService.Setup(s => s.AssignAppointment(It.IsAny<AssignAppointmentDTO>())).ReturnsAsync(() => new HttpResponseMessage(HttpStatusCode.OK));

            //act
            BookingController bookingController = new(mockBookingService.Object, mockLogger.Object);
            IActionResult res = await bookingController.ConfirmAppointment(1);

            // Assert
            OkObjectResult okResult = Assert.IsType<OkObjectResult>(res);
        }

        [Fact]
        public async Task Test_ConfirmAppointment_TransferConflictStatus()
        {
            mockBookingService.Setup(s => s.AssignAppointment(It.IsAny<AssignAppointmentDTO>())).ReturnsAsync(() => new HttpResponseMessage(HttpStatusCode.Conflict));

            //act
            BookingController bookingController = new(mockBookingService.Object, mockLogger.Object);
            IActionResult res = await bookingController.ConfirmAppointment(1);

            // Assert
            ConflictObjectResult okResult = Assert.IsType<ConflictObjectResult>(res);
        }
    }
}