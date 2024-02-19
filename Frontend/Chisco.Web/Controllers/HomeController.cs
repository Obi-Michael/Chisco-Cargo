using Chisco.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Chisco.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CreateShipments()
        {
            return View();
        }
        public IActionResult SalesReport()
        {
            return View();
        }
        public IActionResult FleetReport()
        {
            return View();
        }
        public IActionResult FindShipment()
        {
            return View();
        }
        public IActionResult Users()
        {
            return View();
        }
        public IActionResult Logout()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
