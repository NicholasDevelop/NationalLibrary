﻿using NationalLibrary.Models;
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
			//DataQueries.EditUser("ABSDEL03A03M849U")
			//DataQueries.InsertUser
			//(
			//"ABSDEL03A03M849U", "Librarian", "Mattia", "Romagnoli", "3339998888", new DateTime(1996,10,19),
			//"Siena", "Via Dante Alighieri 8", 20040, "SI",
			//"AA01890BB", "C.I.", "Siena", new DateTime(2030,03,25),
			//"mattiaromagnoli1@gmail.com", "Romans", "password", String.Empty, ctx
			//);
			//ViewsLoaders.UserFinalViewList(ctx);
			return View();
		}

		public IActionResult loginPage()
		{
			return View();
		}
		public IActionResult userDashboard(User user)
		{
			return View(user);
		}
		public IActionResult dashboard(User user)
		{
			string type = ViewsLoaders.getUserType(user.Username, user.Password, ctx);
			if (string.IsNullOrEmpty(type))
			{
				throw new Exception("Utente non esistente");
			}
			switch (type)
			{
				case "Admin":
					return View(user);
				case "User":
					return RedirectToAction("userDashboard", user);
				case "Librarian":
					return View(user);
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