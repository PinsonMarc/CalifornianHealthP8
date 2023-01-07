using Microsoft.AspNetCore.Mvc;

namespace Booking.API.Controllers
{
    public class BookingController : Controller
    {
        protected IRepository _repo;
        private readonly ILogger<BookingController> _logger;

        public BookingController(IRepository repo, ILogger<BookingController> logger)
        {
            _repo = repo;
            _logger = logger;
        }


    }
}