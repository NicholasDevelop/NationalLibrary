using NationalLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using NationalLibrary.Data;
using NationalLibrary.Metodi;
using NationalLibrary.FinalViews;
using System.Diagnostics.Eventing.Reader;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

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
		private static UserFinalView userFinal;
		public IActionResult Index()
		{
			ViewData["UserLogged"] = userFinal;
			//DataQueries.InsertUser("ABSDEL03A03M849U", "Librarian", "Mattia", "Romagnoli", "3339998888", new DateTime(1975, 10, 19),
			//"Genova", "Via Dante Alighieri 8", 20040, "SI",
			//"AA01890BB", "C.I.", "Siena", new DateTime(2030, 03, 25),
			//"mattiaromagnoli1@gmail.com", "RomansLibrarian", "password", String.Empty, ctx);
			//DataQueries.InsertUser
			//(
			//"ADMINR03A03M849U", "Admin", "Mattia", "Romagnoli", "3409998888", new DateTime(1996,12,05),
			//"Siena", "Via Dante Alighieri 8", 69040, "SI",
			//"AD01890MI", "C.I.", "Siena", new DateTime(2030,12,25),
			//"mattiaromagnoli1@gmail.com", "Admin", "Admin", String.Empty, ctx
			//);
			//DataQueries.InsertUser
			//(
			//"USERMR03A03M849U", "User", "Brian", "Romagnoli", "3400768888", new DateTime(2003,01,02),
			//"Siena", "Via Dante Alighieri 8", 69040, "SI",
			//"AZ01890MI", "C.I.", "Siena", new DateTime(2030,12,25),
			//"mattiaromagnoli1@gmail.com", "Brian", "password", String.Empty, ctx
			//);
			//ViewsLoaders.UserFinalViewList(ctx);
			return View();
		}
		public IActionResult router()
		{
			return RedirectToAction("dashboard", userFinal);
		}
		public IActionResult addEmployee(UserFinalView user)
		{
			if (userFinal == null || userFinal.Type.ToLower() == "user" || userFinal.Type.ToLower() == "librarian")
				return RedirectToAction("Error");
			return View(user);
		}
		public IActionResult logout()
		{
			userFinal = null;
			return RedirectToAction("Index");
		}
		public IActionResult loginPage()
		{
			return View();
		}
		public IActionResult userDashboard(UserFinalView user)
		{
			ViewData["Today"] = DateTime.Now;
			return View(user);
		}

		public IActionResult viewBook(BookFinalView book)
		{
			ViewData["UserLogged"] = userFinal;
			return View(book);
		}
		public IActionResult newBook(BookFinalView book)
		{
			if (userFinal == null || userFinal.Type.ToLower() == "user")
				return RedirectToAction("Error");
			return View(book);
		}
		public IActionResult bookList(BookFinalView book)
		{
			ViewData["UserLogged"] = userFinal;
			return View(book);
		}

		public IActionResult insertEmployee(UserFinalView user)
		{
			Console.WriteLine(user.Name);
			Console.WriteLine(user.Username);
			Console.WriteLine(user.MobilePhone);
			DataQueries.InsertUser(user.FiscalCode, "Librarian", user.Name, user.Surname, user.MobilePhone, user.BirthDate, user.City, user.Street,
			user.CAP, user.Province, user.DocumentNumber, user.DocumentType,
			user.ReleasedBy, user.ExpiredOn, user.Email, user.Username, user.Password, String.Empty, ctx);
			return RedirectToAction("dashboard", userFinal);
		}

		public IActionResult insertBook(BookFinalView book)
		{

			return RedirectToAction("dashboard", userFinal);
		}

		public IActionResult dashboard(UserFinalView user)
		{
			UserFinalView type = ViewsLoaders.getUserType(user.Username, user.Password, ctx);
			try
			{
				if (string.IsNullOrEmpty(type.Type))
				{
					throw new Exception("Utente non esistente");
				}
				Console.WriteLine(type.Email);
				switch (type.Type)
				{
					case "Admin":
						userFinal = type;
						List<UserFinalView> result = new List<UserFinalView>();
						foreach (UserFinalView item in ViewsLoaders.UserFinalViewList(ctx))
							if (item.Type.ToLower() == "librarian")
								result.Add(item);
						ViewData["Employees"] = result;
						ViewData["Today"] = DateTime.Now;
						return View(type);
					case "User":
						return RedirectToAction("userDashboard", type);
					case "Librarian":
						return View(type);
				}
			}
			catch (Exception ex)
			{
				return RedirectToAction("Error");
			}
			return View("Index");
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