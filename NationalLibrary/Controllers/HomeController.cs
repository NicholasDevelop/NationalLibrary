using NationalLibrary.Models;
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
		private static UserFinalView temp;
		private static UserRequestFinalView requestTmp;

		public HomeController(ILogger<HomeController> logger, LibraryContext ctx)
		{
			_logger = logger;
			this.ctx = ctx;
		}
		private static UserFinalView userFinal;
		#region Image
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
		private string getImage(BookFinalView book)
		{
			string bytes;
			bytes = string.Format("data:image/jpg;base64,{0}", Convert.ToBase64String(book.CoverImg));

			return bytes;
		}
		#endregion
		public IActionResult Index()
		{
			ViewData["UserLogged"] = userFinal;
			ViewData["Images"] = getImages();
			ViewData["Last5Books"] = Metodi.ViewsLoaders.getLastFiveInsertedBooks(ctx);
			//Console.WriteLine(ViewData["Last5Books"]);
			return View();
		}
		public IActionResult router()
		{
			return RedirectToAction("dashboard", userFinal);
		}
		#region AddSomePerson
		[HttpPost]
		public IActionResult addUser()
		{
			if (userFinal == null || userFinal.Type.ToLower() == "user")
				return RedirectToAction("Error");
			return View();
		}

		public IActionResult addEmployee(UserFinalView user)
		{
			if (userFinal == null || userFinal.Type.ToLower() == "user" || userFinal.Type.ToLower() == "librarian")
				return RedirectToAction("Error");
			return View(user);
		}
		#endregion
		#region Log
		public IActionResult logout()
		{
			userFinal = null;
			return RedirectToAction("Index");
		}
		public IActionResult loginPage()
		{
			return View();
		}
		#endregion

		#region BookRequests
		public IActionResult askForBook(IFormCollection form)
		{
			DataQueries.InsertRequest(userFinal.FiscalCode, form["title"], form["author"], form["comment"], null, ctx);
			return RedirectToAction("router", userFinal);
		}
		public IActionResult acceptBook(Guid id)
		{
			requestTmp = ViewsLoaders.getRequestById(id, ctx);
			return RedirectToAction("newBook");
		}
		public IActionResult discardBook(Guid id)
		{
			DataQueries.UpdateRequestStateReject(id, "Rifiutata", ctx);
			return RedirectToAction("dashboard", userFinal);
		}
		#endregion
		//Controller per la gestione dei Libri
		#region BookRent
		public IActionResult viewBookFromUser(UserFinalView user, Guid id)
		{
			BookFinalView book = DataQueries.getBookByGuid(id, ctx);
			if (DataQueries.IsBookAvaiable(book.ISBN, ctx))
			{
				ViewData["UserLogged"] = userFinal;
				ViewData["Image"] = getImage(book);
				ViewData["BookToRent"] = book;
				return View(user);
			}
			else
			{
				return RedirectToAction("userDashboard", userFinal);
			}
		}
		public IActionResult viewBook(BookFinalView book, Guid id)
		{
			ViewData["UserLogged"] = userFinal;
			book = DataQueries.getBookByGuid(id, ctx);
			ViewData["Image"] = getImage(book);
			return View(book);
		}
		public IActionResult rentBook(UserFinalView rent, Guid id)
		{
			BookFinalView bookToRent = DataQueries.getBookByGuid(id, ctx);
			if (userFinal.Type == "Librarian")
				DataQueries.InsertRent(bookToRent.BookGuid, rent.FiscalCode, ctx);
			else
				DataQueries.InsertRent(bookToRent.BookGuid, userFinal.FiscalCode, ctx);

			return RedirectToAction("dashboard", userFinal);
		}

		public IActionResult bookWithdrawned(Guid id)
		{
			DataQueries.BookDeliveredFromRent(id, ctx);
			return RedirectToAction("dashboard", userFinal);
		}
		public IActionResult bookReturned(Guid id)
		{
			DataQueries.ReturnRent(id, ctx);
			return RedirectToAction("dashboard", userFinal);
		}
		#endregion

		#region Book
		public IActionResult modifyBook(BookFinalView book, Guid id)
		{
			book = DataQueries.getBookByGuid(id, ctx);
			tmp = book;
			ViewData["Image"] = getImage(book);
			return View(book);
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

		public IActionResult deleteBook(BookFinalView book, Guid id)
		{
			book = DataQueries.getBookByGuid(id, ctx);
			DataQueries.DeleteBook(book.BookGuid, ctx);
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
			if (requestTmp != null)
			{
				DataQueries.UpdateRequestState(requestTmp.RequestGuid, "Accettata", book.Title, book.Author, book.PublishingHouse, true, book.Presentation,
				book.Genre, book.CoverImg,
				book.Price, null, null, null, null, book.ISBN, ctx);
				requestTmp = null;
			}
			else
			{
				DataQueries.InsertBook(book.Title, book.Author, book.PublishingHouse, true, book.Presentation,
					book.Genre, book.CoverImg,
					book.Price, book.Room, book.Scaffhold, book.Position, book.Shelf, book.ISBN, ctx);
			}
			return RedirectToAction("dashboard", userFinal);
		}

		#endregion

		#region Employee
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

		public IActionResult modifyEmployee(UserFinalView user, string id)
		{
			user = ViewsLoaders.getUserByFiscalCode(id, ctx);
			temp = user;
			return View(user);
		}

		[HttpPost]
		public IActionResult postModifyEmployee(UserFinalView user)
		{
			user.Password = temp.Password;
			user.FiscalCode = temp.FiscalCode;
			user.Type = temp.Type;
			DataQueries.EditUser(user.FiscalCode, user.Type, user.Name, user.Surname, user.MobilePhone, user.BirthDate, user.FCRelatedTO, user.City, user.Street, user.CAP, user.Province, user.DocumentNumber,
				user.DocumentType, user.ReleasedBy, user.ExpiredOn, user.Email, user.Username, user.Password, ctx);
			return RedirectToAction("dashboard", userFinal);
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
			ViewData["Images"] = getImages();
			ViewData["RentedBooks"] = ViewsLoaders.RentRequestFinalViewList(ctx);
			ViewData["RequestFinalView"] = ViewsLoaders.UserRequestFinalViewList(ctx);
            ViewData["LibraryContext"] = ctx;
            //Console.WriteLine("CTX" + ViewData["LibraryContext"]);
            return View(user);
		}

		public IActionResult deleteEmployee(UserFinalView user, string id)
		{
			user = ViewsLoaders.getUserByFiscalCode(id, ctx);
			DataQueries.DeleteUser(user.FiscalCode, ctx);
			return RedirectToAction("dashboard", userFinal);
		}
		#endregion

		#region User
		public IActionResult userDashboard(UserFinalView user)
		{
			ViewData["Today"] = DateTime.Now;
			ViewData["UserLogged"] = userFinal;
			ViewData["Images"] = getImages();
			List<BookFinalView> l = ViewsLoaders.BookFinalViewList(ctx);
			ViewData["Books"] = l;

			List<WaitingBookStatusFinalView> wlist = DataQueries.CheckIfBookArrived(user.FiscalCode, ctx);
			ViewData["wlist"] = wlist;
			return View(user);
		}
		public IActionResult insertUser(UserFinalView user)
		{
			try
			{

				if (string.IsNullOrEmpty(user.FCRelatedTO) && (DateTime.Now - user.BirthDate).TotalDays > 6570)
				{
					DataQueries.InsertUser(user.FiscalCode, "User", user.Name, user.Surname, user.MobilePhone, user.BirthDate, user.City, user.Street,
					user.CAP, user.Province, user.DocumentNumber, user.DocumentType,
					user.ReleasedBy, user.ExpiredOn, user.Email, user.Username, user.Password, String.Empty, ctx);
					return RedirectToAction("dashboard", userFinal);
				}
				else
				{
					if (DataQueries.CheckFCExsist(user.FCRelatedTO, ctx))
					{
						DataQueries.InsertUser(user.FiscalCode, "User", user.Name, user.Surname, user.MobilePhone, user.BirthDate, user.City, user.Street,
						user.CAP, user.Province, user.DocumentNumber, user.DocumentType,
						user.ReleasedBy, user.ExpiredOn, user.Email, user.Username, user.Password, user.FCRelatedTO, ctx);
					}
					else
					{
						return RedirectToAction("addUser");
					}
					return RedirectToAction("dashboard", userFinal);
				}
			}
			catch (Exception ex)
			{
				return Error();
			}
		}
		//public static UserFinalView getUserByFiscalCode(string FiscalCode, LibraryContext ctx)
		//{
		//    UserFinalView user = new UserFinalView();
		//    foreach (var item in ViewsLoaders.UserFinalViewList(ctx))
		//    {
		//        if (item.FiscalCode == FiscalCode)
		//        {
		//            user = item;
		//            break;
		//        }
		//    }
		//    return user;
		//}
		public IActionResult modifyUser(UserFinalView user, string id)
		{
			user = ViewsLoaders.getUserByFiscalCode(id, ctx);
			temp = user;
			return View(user);
		}

		[HttpPost]
		public IActionResult postModifyUser(UserFinalView user)
		{
			user.Password = temp.Password;
			user.FiscalCode = temp.FiscalCode;
			user.Type = temp.Type;
			DataQueries.EditUser(user.FiscalCode, user.Type, user.Name, user.Surname, user.MobilePhone, user.BirthDate, user.FCRelatedTO, user.City, user.Street, user.CAP, user.Province, user.DocumentNumber,
				user.DocumentType, user.ReleasedBy, user.ExpiredOn, user.Email, user.Username, user.Password, ctx);
			return RedirectToAction("employeeDashboard", userFinal);
		}

		public IActionResult deleteUser(UserFinalView user, string id)
		{
			user = ViewsLoaders.getUserByFiscalCode(id, ctx);
			DataQueries.DeleteUser(user.FiscalCode, ctx);
			return RedirectToAction("employeeDashboard", userFinal);
		}
		#endregion

		public IActionResult dashboard(UserFinalView user)
		{
			UserFinalView type = ViewsLoaders.getUserType(user.Username, user.Password, ctx);
			//Console.WriteLine(type.Type);
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
						ViewData["RentedBooks"] = ViewsLoaders.RentRequestFinalViewList(ctx);
						ViewData["LastSignedUsers"] = DataQueries.getLastMonthRegisteredUsers(ctx);
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