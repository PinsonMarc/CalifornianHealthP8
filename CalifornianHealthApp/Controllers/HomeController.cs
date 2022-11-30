
using CalifornianHealthApp.Models.DTOs;
using CalifornianHealthApp.Models.Entities;
using CalifornianHealthApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CalifornianHealthApp.Controllers
{
    
    public class HomeController : Controller
    {

        protected IRepository _repo;
        protected ILogger<BookingController> _logger;

        public HomeController(IRepository repo, ILogger<BookingController> logger)
        {
            _repo = repo;
            _logger = logger;
        }
        public ActionResult Index()
        {
            ConsultantModelList conList = new ConsultantModelList();
            List<Consultant> cons = new List<Consultant>();

            cons = _repo.FetchConsultants();
            conList.ConsultantsList = new SelectList(cons, "Id", "FName");
            conList.consultants = cons;
            return View(conList);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}