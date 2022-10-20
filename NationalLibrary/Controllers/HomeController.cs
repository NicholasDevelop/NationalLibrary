﻿using NationalLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using NationalLibrary.Data;
using NationalLibrary.Metodi;
using NationalLibrary.FinalViews;
using System.Diagnostics.Eventing.Reader;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using static System.Net.Mime.MediaTypeNames;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using System.Reflection.Metadata;
using System.Linq.Expressions;
using System.Data;

namespace NationalLibrary.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly LibraryContext ctx;
		private static BookFinalView tmp;

		public HomeController(ILogger<HomeController> logger, LibraryContext ctx)
		{
			_logger = logger;
			this.ctx = ctx;
		}
		private static UserFinalView userFinal;
		private List<string> getImages()
		{
			List<BookFinalView> result2 = new List<BookFinalView>();
			foreach (BookFinalView item in ViewsLoaders.BookFinalViewList(ctx))
				result2.Add(item);
			ViewData["Books"] = result2;

			List<string> bytes = new List<string>();

			foreach (BookFinalView item in ViewsLoaders.BookFinalViewList(ctx))
			{
				bytes.Add(string.Format("data:image/jpg;base64,{0}", Convert.ToBase64String(item.CoverImg)));
			}
			return bytes;
		}
		public IActionResult addUser()
		{
			if (userFinal == null || userFinal.Type.ToLower() == "user")
				return RedirectToAction("Error");
			return View();
		}
		private string getImage(BookFinalView book)
		{
			string bytes;
			bytes = string.Format("data:image/jpg;base64,{0}", Convert.ToBase64String(book.CoverImg));

			return bytes;
		}

		public IActionResult Index()
		{
			ViewData["UserLogged"] = userFinal;
			ViewData["Images"] = getImages();
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
			ViewData["UserLogged"] = userFinal;
			ViewData["Images"] = getImages();
			List<BookFinalView> l = ViewsLoaders.BookFinalViewList(ctx);
			ViewData["Books"] = l;
			return View(user);
		}


		//Controller per la gestione dei Libri
		public IActionResult viewBook(BookFinalView book, Guid id)
		{
			ViewData["UserLogged"] = userFinal;
			book = DataQueries.getBookByGuid(id, ctx);
			ViewData["Image"] = getImage(book);
			return View(book);
		}

		public IActionResult modifyBook(BookFinalView book, Guid id)
		{
			book = DataQueries.getBookByGuid(id, ctx);
			tmp = book;
			ViewData["Image"] = getImage(book);
			return View(book);
		}
		public IActionResult deleteBook(BookFinalView book, Guid id)
		{
			book = DataQueries.getBookByGuid(id, ctx);
			DataQueries.DeleteBook(book.BookGuid, ctx);
			return RedirectToAction("dashboard", userFinal);
		}

		[HttpPost]
		public IActionResult postModifyBook(BookFinalView book)
		{
			foreach (var file in Request.Form.Files)
			{
				IFormFile img = file;
				byte[] p1 = null;
				using (var ms1 = new MemoryStream())
				{
					img.CopyTo(ms1);
					p1 = ms1.ToArray();
				}
				tmp.CoverImg = p1;
			}
			book.BookGuid = tmp.BookGuid;
			book.CoverImg = tmp.CoverImg;
			DataQueries.EditBook(book.BookGuid, book.Title, book.Author, book.PublishingHouse, true, book.Presentation, book.Genre, book.CoverImg, book.Room, book.Scaffhold, book.Shelf, book.Position, book.ISBN, book.Price, ctx);
			return RedirectToAction("dashboard", userFinal);
		}


		public IActionResult bookList(BookFinalView book)
		{
			ViewData["UserLogged"] = userFinal;
			ViewData["Images"] = getImages();
			return View(book);
		}

		public IActionResult newBook(BookFinalView book)
		{
			if (userFinal == null || userFinal.Type.ToLower() == "user")
				return RedirectToAction("Error");
			return View(book);
		}

		/// pagina di controllo dei dati che provengono da newBook per la dashboard
		/// </summary>
		/// <param name="book"></param>
		/// <returns></returns>
		[HttpPost]
		public IActionResult insertBook(BookFinalView book)
		{
			foreach (var file in Request.Form.Files)
			{
				IFormFile img = file;
				byte[] p1 = null;
				using (var ms1 = new MemoryStream())
				{
					img.CopyTo(ms1);
					p1 = ms1.ToArray();
				}
				book.CoverImg = p1;
			}
			DataQueries.InsertBook(book.Title, book.Author, book.PublishingHouse, true, book.Presentation,
				book.Genre, book.CoverImg, DateTime.Now,
				book.Price, book.Room, book.Scaffhold, book.Position, book.Shelf, book.ISBN, ctx);
			return RedirectToAction("dashboard", userFinal);
		}



		//Controller per la gestione degli impiegati
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
		public void insertUser(UserFinalView user)
		{
			try
			{

				if (string.IsNullOrEmpty(user.FCRelatedTO) && (DateTime.Now - user.BirthDate).TotalDays > 65070)
				{
					DataQueries.InsertUser(user.FiscalCode, "User", user.Name, user.Surname, user.MobilePhone, user.BirthDate, user.City, user.Street,
					user.CAP, user.Province, user.DocumentNumber, user.DocumentType,
					user.ReleasedBy, user.ExpiredOn, user.Email, user.Username, user.Password, String.Empty, ctx);
					router();
				}
				else
				{
					if (DataQueries.CheckFCExsist(user.FCRelatedTO, ctx))
					{
						DataQueries.InsertUser(user.FiscalCode, "User", user.Name, user.Surname, user.MobilePhone, user.BirthDate, user.City, user.Street,
						user.CAP, user.Province, user.DocumentNumber, user.DocumentType,
						user.ReleasedBy, user.ExpiredOn, user.Email, user.Username, user.Password, user.FCRelatedTO, ctx);
					}
					router();
				}
			}
			catch (Exception ex)
			{
				Error();
			}
		}

		public IActionResult employeeDashboard(UserFinalView user)
		{
			List<UserFinalView> users = new List<UserFinalView>();
			foreach (UserFinalView item in ViewsLoaders.UserFinalViewList(ctx))
				if (item.Type.ToLower() == "user")
					users.Add(item);
			ViewData["Users"] = users;
			ViewData["Today"] = DateTime.Now;
			List<BookFinalView> result2 = new List<BookFinalView>();
			foreach (BookFinalView item in ViewsLoaders.BookFinalViewList(ctx))
				result2.Add(item);
			ViewData["Books"] = result2;
			List<RentRequestFinalView> rentedBooks = new List<RentRequestFinalView>();
			foreach (RentRequestFinalView item in ViewsLoaders.RentRequestFinalViewList(ctx))
				rentedBooks.Add(item);
			ViewData["RentedBook"] = rentedBooks;

			return View(user);
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
						List<BookFinalView> result2 = new List<BookFinalView>();
						foreach (BookFinalView item in ViewsLoaders.BookFinalViewList(ctx))
							result2.Add(item);
						ViewData["Books"] = result2;
						List<BookFinalView> lastBuyedBooks = ViewsLoaders.MonthBookBought(ctx);
						ViewData["LastBuyedBooks"] = lastBuyedBooks;
						List<UserFinalView> lastSignedUsers = new List<UserFinalView>();
						lastSignedUsers = DataQueries.getLastMonthRegisteredUsers(ctx);
						ViewData["LastSignedUsers"] = lastSignedUsers;
						return View(type);
					case "User":
						userFinal = type;
						return RedirectToAction("userDashboard", type);
					case "Librarian":
						userFinal = type;
						return RedirectToAction("employeeDashboard", type);
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