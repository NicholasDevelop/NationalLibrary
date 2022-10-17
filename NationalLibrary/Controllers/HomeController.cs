using Microsoft.AspNetCore.Mvc;
using NationalLibrary.Models;
using System.Diagnostics;

namespace NationalLibrary.Controllers
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

            //    DataQueries.InsertUser
            //(
            //     "A", "A", "A", "A", "A", DateTime.Now,
            //     "A", "A", 1, "A",
            //     "AA11111AA", "A", "A", DateTime.Now,
            //     "AA11111AA", "A", "A", String.Empty, ctx
            //);





            return View();
        }

        public IActionResult loginPage()
        {
            return View();
        }
        public IActionResult dashboard()
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