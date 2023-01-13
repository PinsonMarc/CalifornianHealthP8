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

        private Mock<IBookingService> _mockBookingService;
        private Mock<ILogger<BookingController>> _mockLogger;

        public BookingControllerTests()
        {
            //Arrange
            _mockLogger = new();
            _mockBookingService = new();
        }

        [Fact]
        public async Task Test_FetchConsultants_ReturnsViewResult()
        {
            //Arrange
            _mockBookingService.Setup(s => s.FetchConsultantCalendars()).ReturnsAsync(() => new List<ConsultantCalendarDTO>());

            //act
            BookingController bookingController = new(_mockBookingService.Object, _mockLogger.Object);
            ActionResult res = await bookingController.GetConsultantCalendar();

            //assert
            Assert.IsType<ViewResult>(res);
        }


        [Fact]
        public async Task Test_GetConsultantAppointments_ReturnsOkObjectResult()
        {
            //Arrange
            string expected = "expected";
            _mockBookingService.Setup(s => s.GetConsultantAppointments(It.IsAny<ConsultantDailyAppointmentsDTO>())).ReturnsAsync(() => expected);

            //act
            BookingController bookingController = new(_mockBookingService.Object, _mockLogger.Object);
            IActionResult res = await bookingController.ConsultantAppointments(new ConsultantDailyAppointmentsDTO());

            // Assert
            OkObjectResult okResult = Assert.IsType<OkObjectResult>(res);
            Assert.Equal(expected, okResult.Value);
        }

        [Fact]
        public async Task Test_ConfirmAppointment_ReturnsOkObjectResult()
        {
            //Arrange
            _mockBookingService.Setup(s => s.AssignAppointment(It.IsAny<AssignAppointmentDTO>())).ReturnsAsync(() => new HttpResponseMessage(HttpStatusCode.OK));

            //act
            BookingController bookingController = new(_mockBookingService.Object, _mockLogger.Object);
            IActionResult res = await bookingController.ConfirmAppointment(1);

            // Assert
            OkResult okResult = Assert.IsType<OkResult>(res);
        }

        [Fact]
        public async Task Test_ConfirmAppointment_TransferConflictStatus()
        {
            //Arrange
            _mockBookingService.Setup(s => s.AssignAppointment(It.IsAny<AssignAppointmentDTO>())).ReturnsAsync(() => new HttpResponseMessage(HttpStatusCode.Conflict));

            //act
            BookingController bookingController = new(_mockBookingService.Object, _mockLogger.Object);
            IActionResult res = await bookingController.ConfirmAppointment(1);

            // Assert
            Assert.IsType<ConflictObjectResult>(res);
        }

        [Fact]
        public void Test_GetConfirmAppointment_ReturnsViewResult()
        {
            //act
            BookingController bookingController = new(_mockBookingService.Object, _mockLogger.Object);
            IActionResult res = bookingController.ConfirmAppointment();

            // Assert
            Assert.IsType<ViewResult>(res);
        }
    }
}