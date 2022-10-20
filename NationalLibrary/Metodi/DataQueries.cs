using Microsoft.AspNetCore.Components.RenderTree;
using NationalLibrary.Data;
using NationalLibrary.FinalViews;
using System;
using System.Diagnostics;
using System.IO.Pipelines;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;

namespace NationalLibrary.Metodi
{
	public class DataQueries
	{
		//////////////////  QUERY MANIPOLAZIONE UTENTI   \\\\\\\\\\\\\\\\\\\\\
		public static void InsertUser(string FiscalCode, string Type, string Name, string Surname, string MobilePhone, DateTime BirthDate, string City, string Street, int? CAP, string Province, string DocumentNumber, string DocumentType, string ReleasedBy, DateTime ExpireOn, string Email, string Username, string Password, string FCRelatedTO, LibraryContext ctx)
		{
			// ctx = new LibraryContext();
			var newuser = new Person() { FiscalCode = FiscalCode, Name = Name, Surname = Surname, Type = Type, MobilePhone = MobilePhone, BirthDate = BirthDate, FCRelatedTO = FCRelatedTO, SignUpDate = DateTime.Now };
			newuser.Residence = new Residence() { City = City, Street = Street, CAP = CAP, Province = Province, AddressGuid = Guid.NewGuid() };
			newuser.Document = new Document() { DocumentNumber = DocumentNumber, DocumentType = DocumentType, ReleasedBy = ReleasedBy, ExpireOn = ExpireOn };
			newuser.User = new User() { Email = Email, Username = Username, Password = Password };

			//Trace.WriteLine(newuser);
			//Trace.WriteLine(newuser.Residence);
			// DA TESTARE !!!
			ctx.People.Add(newuser);
			ctx.Users.Add(newuser.User);
			ctx.Documents.Add(newuser.Document);
			ctx.Residences.Add(newuser.Residence);
			ctx.SaveChanges();
		}
		public static void InsertTutorUser(string FiscalCode, string Type, string Name, string Surname, string MobilePhone, DateTime BirthDate, string DocumentNumber, string FCRelatedTO, LibraryContext ctx)

		{
			var newuser = new Person() { FiscalCode = FiscalCode, Name = Name, Surname = Surname, MobilePhone = MobilePhone, BirthDate = BirthDate, SignUpDate = DateTime.Now, FCRelatedTO = FCRelatedTO };
			newuser.Document = new Document() { DocumentNumber = DocumentNumber };
			newuser.Residence = new Residence() { AddressGuid = Guid.NewGuid() };

			// DA TESTARE !!!
			ctx.People.Add(newuser);
			ctx.Residences.Add(newuser.Residence);
			ctx.Documents.Add(newuser.Document);
			ctx.SaveChanges();
		}
		public static void DeleteUser(string FiscalCode, LibraryContext ctx)
		{
			//Seleziono tutti quelli che ci sono nei contatti e la trasformo nella lista

			User view2 = ctx.Users.Where(u => u.FiscalCode == FiscalCode).ToList()[0];
			Person view = ctx.People.Where(u => u.FiscalCode == FiscalCode).ToList()[0];
			ctx.Users.Remove(view2);
			ctx.People.Remove(view);

			ctx.SaveChanges();

		}
		public static void EditUser(string FiscalCode, string Type, string Name, string Surname, string MobilePhone, DateTime BirthDate, string FCRelatedTo, string City, string Street, int CAP, string Province, string DocumentNumber, string DocumentType, string ReleasedBy, DateTime ExpireOn, string Email, string Username, string Password, LibraryContext ctx)
		{

			Person a = ctx.People.Where(u => u.FiscalCode == FiscalCode).ToList()[0];
			List<Residence> b = ctx.Residences.Where(u => a.AddressGuidFK == u.AddressGuid).ToList();
			List<Document> c = ctx.Documents.Where(u => a.DocumentNumberFK == u.DocumentNumber).ToList();
			List<User> d = ctx.Users.Where(u => a.FiscalCode == u.FiscalCode).ToList();

			a.FiscalCode = FiscalCode;
			a.Type = Type;
			
			a.Name = Name;
			a.Surname = Surname;
			a.MobilePhone = MobilePhone;
			a.BirthDate = BirthDate;
			a.FCRelatedTO = FCRelatedTo;
			b[0].City = City;
			b[0].Street = Street;
			b[0].CAP = CAP;
			b[0].Province = Province;
			c[0].DocumentNumber = DocumentNumber;
			c[0].DocumentType = DocumentType;
			c[0].ReleasedBy = ReleasedBy;
			c[0].ExpireOn = ExpireOn;
			d[0].Email = Email;
			d[0].Username = Username;
			d[0].Password = Password;

			ctx.SaveChanges();




		}



		//////////////////  QUERY MANIPOLAZIONE LIBRI   \\\\\\\\\\\\\\\\\\\\\\
		public static void InsertBook(string Title, string Author, string PublishingHouse, bool Available, string Presentation, string Genre, byte[] Coverimg, DateTime BuyDate, string Price, string Room, string Scaffhold, int? Position, string Shelf, string ISBN, LibraryContext ctx)

		{
			var newbook = new Book() { BookGuid = Guid.NewGuid(), Title = Title, Author = Author, PublishingHouse = PublishingHouse, Available = Available, Presentation = Presentation, Genre = Genre, CoverImg = Coverimg, BuyDate = DateTime.Now, Price = Price, ISBNFK = ISBN };
			newbook.Location = new Location() { Room = Room, Schaffold = Scaffhold, Shelf = Shelf, Position = Position };

			string checkforisbn = newbook.ISBNFK;
			bool check = CheckISBNExsist(checkforisbn, ctx);

			if (!check)
			{
				InsertISBN(checkforisbn, ctx);
				ctx.SaveChanges();
			}

			ctx.Books.Add(newbook);
			ctx.Locations.Add(newbook.Location);
			ctx.SaveChanges();

		}
		public static void DeleteBook(Guid BookGuid, LibraryContext ctx)
		{
			//Seleziono tutti quelli che ci sono nei contatti e la trasformo nella lista

			Book view = ctx.Books.Where(u => u.BookGuid == BookGuid).ToList()[0];
			ISBNList a = ctx.ISBNLists.Where(u => u.ISBN == view.ISBNFK).ToList()[0];
			ctx.Books.Remove(view);

			TryISBNDelete(a.ISBN, ctx);

			ctx.SaveChanges();

		}
		public static void EditBook(Guid BookGuid, string Title, string Author, string PublishingHouse, bool Available, string Presentation, string Genre, byte[] Coverimg, string Room, string Scaffhold, string Shelf, int? Position, string ISBN,string Price, LibraryContext ctx)
		{

			Book a = ctx.Books.Where(u => u.BookGuid == BookGuid).ToList()[0];
			Location b = ctx.Locations.Where(u => u.LocationGuid == a.LocationGuidFK).ToList()[0];
			ISBNList i = ctx.ISBNLists.Where(u => u.ISBN == a.ISBNFK).ToList()[0];
			List<WaitingList> w = ctx.WaitingLists.Where(u => u.ISBNFK == i.ISBN).ToList();

			string oldisbn = i.ISBN;

			// Modifiche al libro
			a.Title = Title;
			a.Author = Author;
			a.PublishingHouse = PublishingHouse;
			a.Available = Available;
			a.Presentation = Presentation;
			a.Genre = Genre;
			a.CoverImg = Coverimg;
			a.Price = Price;
			b.Room = Room;
			b.Schaffold = Scaffhold;
			b.Shelf = Shelf;
			b.Position = Position;


			//Controllo se ISBN vecchio != ISBN nuovo. Se sono diversi controllo se il nuovo gia esiste nella lista. Se non c'è lo creo
			//Successivamente controllo se ci sono altri libri presenti sotto il vecchio ISBN. Se ce ne sono allora non è necessario cambiare
			//gli ISBN presenti in waiting list in quanto  sono in attesa dell'isbn giusto. Se non ci sono altri libri sotto il vecchio isbn
			//allora tutti quelli che erano in attesa stavano aspettando per l'ISBN sbagliato. In questo caso faccio un Count di waiting list e
			//se ci sono attese inserite per il vecchio ISBN lo aggiorno con quello nuovo. Finito il ciclo elimino il vecchio ISBN

			if (i.ISBN != ISBN)
			{
				if (!CheckISBNExsist(ISBN, ctx))
				{
					InsertISBN(ISBN, ctx);
				}


				i.ISBN = ISBN;


				if (!ISBNLeft(oldisbn, ctx))
				{
					if (w.Count > 0)
					{
						for (int c = 0; c < w.Count; c++)
						{
							w[c].ISBNFK = ISBN;
						}
					}
					TryISBNDelete(oldisbn, ctx);
				}
			}
			ctx.SaveChanges();
		}
		public static bool CheckISBNExsist(string ISBN, LibraryContext ctx)
		{
			bool check = false;

			List<ISBNList> a = ctx.ISBNLists.Where(u => u.ISBN == ISBN).ToList();
			if (a.Count > 0) { check = true; }

			return check;
		}
		public static void InsertISBN(string ISBN, LibraryContext ctx)
		{
			var newisbn = new ISBNList() { ISBN = ISBN };
			ctx.ISBNLists.Add(newisbn);

		}
		public static void UpdateBookAvailable(Guid BookGuid, bool Available, LibraryContext ctx)
		{
			Book a = ctx.Books.Where(u => u.BookGuid == BookGuid).ToList()[0];
			a.Available = Available;
			ctx.SaveChanges();

		}
		public static bool IsBookAvaiable(string ISBN, LibraryContext ctx)
		{
			List<Book> c = ctx.Books.Where(u => u.ISBNFK == ISBN && u.Available == true).ToList();
			bool check;

			if (c != null)
			{
				check = true;
			}
			else
			{
				check = false;
			}

			return check;

		}
		public static int CountBookAvaiable(string ISBN, LibraryContext ctx)
		{
			List<Book> c = ctx.Books.Where(u => u.ISBNFK == ISBN && u.Available == true).ToList();
			int available;

			available = c.Count();

			return available;


		}
		public static bool ISBNLeft(string ISBN, LibraryContext ctx)
		{
			bool check = false;

			if (!CheckISBNExsist(ISBN, ctx)) { check = false; }
			if (CheckISBNExsist(ISBN, ctx)) { check = true; }

			// false = L'ISBN è presente in lista ma non ci sono libri associati
			// true = L'ISBN è presente in lista ma ci sono ancora libri associati


			return check;

		}
		public static void TryISBNDelete(string ISBN, LibraryContext ctx)
		{
			if (CheckISBNExsist(ISBN, ctx))
			{
				if (ISBNLeft(ISBN, ctx))
				{
					ISBNList a = ctx.ISBNLists.Where(u => u.ISBN == ISBN).ToList()[0];
					ctx.ISBNLists.Remove(a);
					ctx.SaveChanges();
				}
			}

		}
		public static void EditISBNFromISBNList(string ISBN, LibraryContext ctx)
		{
            ISBNList a = ctx.ISBNLists.Where(u => u.ISBN == ISBN).ToList()[0];
			a.ISBN = ISBN;
			ctx.SaveChanges();
        }
		public static BookFinalView getBookByGuid(Guid BookGuid, LibraryContext ctx)
		{
			BookFinalView book = new BookFinalView();
			foreach (var item in ViewsLoaders.BookFinalViewList(ctx))
			{
				if (item.BookGuid == BookGuid)
				{
					book = item;
					break;
				}
			}
			return book;
		}

		//////////////////  QUERY MANIPOLAZIONE AFFITTI   \\\\\\\\\\\\\\\\\\\\\\
		public static void InsertRent(Guid BookGuid, string FiscalCode, LibraryContext ctx)
		{
			// Inserisco nuovo affitto
			var newrent = new Rent() { RentGuid = Guid.NewGuid(), BookGuidFK = BookGuid, FiscalCodeFK = FiscalCode, WithdrawnOn = DateTime.Now };
			ctx.Rents.Add(newrent);

			// Modifico Available del libro
			Book a = ctx.Books.Where(u => u.BookGuid == BookGuid).ToList()[0];
			a.Available = false;
			ctx.SaveChanges();
		}
		public static void ReturnRent(Guid RentGuid, LibraryContext ctx)
		{

			Rent a = ctx.Rents.Where(u => u.RentGuid == RentGuid).ToList()[0];
			List<Book> b = ctx.Books.Where(u => u.BookGuid == a.BookGuidFK).ToList();

			a.ReturnedOn = DateTime.Now;
			b[0].Available = true;

			ctx.SaveChanges();

		}


		//////////////////   QUERY MANIPOLAZIONE REQUEST    \\\\\\\\\\\\\\\\\\\\\\
		public static void InsertRequest(string FiscalCode, string Title, string Author, string Comment, string ISBN, LibraryContext ctx)
		{
			var newrequest = new Request() { RequestGuid = Guid.NewGuid(), FiscalCodeFK = FiscalCode, Title = Title, Author = Author, Comment = Comment, State = "In Lavorazione", ISBN = ISBN, RequestDate = DateTime.Now };
			ctx.Requests.Add(newrequest);
			ctx.SaveChanges();

		}
		public static void UpdateRequestState(Guid RequestGuid, string StateUpdate, string Title, string Author, string PublishingHouse, bool Available, string Presentation, string Genre, byte[] Coverimg, DateTime BuyDate, string Price, string Room, string Scaffhold, int? Position, string Shelf, string ISBN, LibraryContext ctx)
		{
			if (StateUpdate == "Accettata")
			{
				bool check = CheckISBNExsist(ISBN, ctx);

				if (!check)
				{
					InsertISBN(ISBN, ctx);
				}

				InsertBook(Title, Author, PublishingHouse, Available, Presentation, Genre, Coverimg, BuyDate, Price, Room, Scaffhold, Position, Shelf, ISBN, ctx);

				Request a = ctx.Requests.Where(u => u.RequestGuid == RequestGuid).ToList()[0];
				string FC = a.FiscalCodeFK;
				a.State = StateUpdate;

				InsertWaiting(FC, ISBN, ctx);
				ctx.SaveChanges();

			}
			else
			{
				Request a = ctx.Requests.Where(u => u.RequestGuid == RequestGuid).ToList()[0];
				a.State = StateUpdate;
				ctx.SaveChanges();

			}



		}


		//////////////////   QUERY MANIPOLAZIONE ATTESE    \\\\\\\\\\\\\\\\\\\\\\
		public static void InsertWaiting(string FiscalCode, string ISBN, LibraryContext ctx)
		{
			var newwaiting = new WaitingList() { WaitingGuid = Guid.NewGuid(), FiscalCodeFK = FiscalCode, RequestedOn = DateTime.Now, ISBNFK = ISBN };
			ctx.WaitingLists.Add(newwaiting);
			ctx.SaveChanges();

		}
		public static void BookDelivered(Guid WaitingGuid, LibraryContext ctx)
		{
			WaitingList a = ctx.WaitingLists.Where(u => u.WaitingGuid == WaitingGuid).ToList()[0];
			ISBNList b = ctx.ISBNLists.Where(u => u.ISBN == a.ISBNFK).ToList()[0];
			List<Book> c = ctx.Books.Where(u => u.ISBNFK == b.ISBN && u.Available == true).ToList();

			a.ReceivedOn = DateTime.Now;

			string ISBN = a.ISBNFK;
			Guid BookGuid = c[0].BookGuid;
			string FiscalCode = a.FiscalCodeFK;


			InsertRent(BookGuid, FiscalCode, ctx);
		}


		////////////////       QUERY AVVISI  UTENTE       \\\\\\\\\\\\\\\\\\\\\\
		public static List<WaitingBookStatusFinalView> CheckIfBookArrived(string FiscalCode, LibraryContext ctx)
		{
			List<WaitingBookStatusFinalView> lista = new List<WaitingBookStatusFinalView>();


			Person p = ctx.People.Where(u => u.FiscalCode == FiscalCode).ToList()[0];
			List<WaitingList> w = ctx.WaitingLists.Where(u => u.FiscalCodeFK == p.FiscalCode).ToList();
			//Rent r = ctx.Rents.Where(u => u.FiscalCodeFK == FiscalCode).ToList()[0];



			string Name = p.Name;

			for (int e = 0; e < w.Count; e++)
			{
				ISBNList l = ctx.ISBNLists.Where(u => u.ISBN == w[e].ISBNFK).ToList()[0];
				List<Book> b = ctx.Books.Where(u => u.ISBNFK == l.ISBN).ToList();
				foreach (var item in b)
				{
					if (item.Available)
					{

						string Title = item.Title;
						lista.Add(new WaitingBookStatusFinalView(Name, Title));
					}
				}

			}
			return lista;

		}

		// Ritorna la lista degli utenti registrati nell'ultimo mese
		public static List<UserFinalView> getLastMonthRegisteredUsers(LibraryContext ctx)
		{
			var thismonth = DateTime.Now.ToString("MM yyyy");
			var month = thismonth.Split(' ')[0];
			var year = thismonth.Split(' ')[1];

			List<UserFinalView> list = new List<UserFinalView>();

			foreach (var item in ViewsLoaders.UserFinalViewList(ctx))
			{
				if (item.Type.ToLower() == "user")
				{
					var registerDate = item.SignUpDate.ToString().Split(' ')[0];
					if (registerDate.Split('/')[1] == month && registerDate.Split('/')[2] == year)
						list.Add(item);
				}
			}

			return list;
		}
	}

}


