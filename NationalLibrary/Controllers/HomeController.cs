using NationalLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using NationalLibrary.Data;
using NationalLibrary.Metodi;
using NationalLibrary.FinalViews;

namespace NationalLibrary.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly LibraryContext ctx;
		public HomeController(ILogger<HomeController> logger, LibraryContext ctx)
		{
			_logger = logger;
			this.ctx = ctx;
		}

		public IActionResult Index()
		{
			DataQueries.InsertUser
			(
			"ABSCNL03A03M849U", "Admin", "Brian", "Romagnoli", "3339998888", new DateTime(1996,10,19),
			"Genova", "Via Dante Alighieri 8", 20040, "GE",
			"AA00000BB", "C.I.", "Genova", new DateTime(2030,03,25),
			"brianromagnoli1@gmail.com", "Admin", "Admin", String.Empty, ctx
			);
			ViewsLoaders.UserFinalViewList(ctx);
			DataQueries.DeleteUser("A", ctx);
			return View();
		}

		public IActionResult loginPage()
		{
			return View();
		}
		public IActionResult dashboard(User user)
		{
			if (string.IsNullOrEmpty(ViewsLoaders.getUserType(user.Username, user.Password, ctx)))
			{
				throw new Exception("Utente non esistente");
			}
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