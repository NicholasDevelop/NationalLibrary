using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.WebEncoders.Testing;
using NationalLibrary.Data;
using NationalLibrary.FinalViews;
using System.Globalization;
using System.Runtime.Serialization.Formatters;

namespace NationalLibrary.Metodi
{
	public class ViewsLoaders
	{
		//private static readonly LibraryContext ctx;

		public static IQueryable<UserFinalView> ViewsLoader_Users(LibraryContext ctx)
		{

			var usersview = from x in ctx.People  // seleziona tutto nella lista CONTACTS presente in CONTEXT
							join a in ctx.Users on x.FiscalCode equals a.FiscalCode
							join b in ctx.Residences on x.AddressGuidFK equals b.AddressGuid
							join c in ctx.Documents on x.DocumentNumberFK equals c.DocumentNumber
							// In pratica joino solo i dati dove x.qualcosa (la select iniziale) è uguale alla FK del dato stesso


							// Creo un nuovo oggetto FinalView dove metto dentro tutti i risultati della query
							select new UserFinalView
							{
								FiscalCode = x.FiscalCode,
								Type = x.Type,
								Name = x.Name,
								Surname = x.Surname,
								BirthDate = x.BirthDate,
								SignUpDate = x.SignUpDate,
								MobilePhone = x.MobilePhone,
								FCRelatedTO = x.FCRelatedTO,
								Email = a.Email,
								City = b.City,
								Street = b.Street,
								CAP = b.CAP,
								Province = b.Province,
								DocumentNumber = c.DocumentNumber,
								DocumentType = c.DocumentType,
								ReleasedBy = c.ReleasedBy,
								ExpiredOn = c.ExpireOn,
								Username = a.Username,
								Password = a.Password
							};

			return usersview;
		}

		public static IQueryable<BookFinalView> ViewsLoader_Books(LibraryContext ctx)
		{
			var booksview = from x in ctx.Books  // seleziona tutto nella lista CONTACTS presente in CONTEXT
							join a in ctx.Locations on x.LocationGuidFK equals a.LocationGuid
							join i in ctx.ISBNLists on x.ISBNFK equals i.ISBN


							// Creo un nuovo oggetto FinalView dove metto dentro tutti i risultati della query
							select new BookFinalView
							{
								BookGuid = x.BookGuid,
								Title = x.Title,
								Author = x.Author,
								PublishingHouse = x.PublishingHouse,
								Available = x.Available,
								Presentation = x.Presentation,
								Genre = x.Genre,
								BuyDate = x.BuyDate,
								Price = x.Price,
								CoverImg = x.CoverImg,
								Room = a.Room,
								Scaffhold = a.Schaffold,
								Position = a.Position,
								ISBN = i.ISBN,
								Shelf = a.Shelf
							};

			return booksview;
		}

		public static List<UserRequestFinalView> waitingFinalViewsList(LibraryContext ctx)
		{
			var rentsview = from x in ctx.Books
							join a in ctx.Rents on x.BookGuid equals a.BookGuidFK
							join b in ctx.ISBNLists on x.ISBNFK equals b.ISBN
							join c in ctx.People on a.FiscalCodeFK equals c.FiscalCode
							join d in ctx.WaitingLists on c.FiscalCode equals d.FiscalCodeFK
							join e in ctx.Requests on c.FiscalCode equals e.FiscalCodeFK
							join f in ctx.Users on c.FiscalCode equals f.FiscalCode

							// Creo un nuovo oggetto FinalView dove metto dentro tutti i risultati della query
							select new UserRequestFinalView
							{
								//BookGuid = x.BookGuid,

								//ISBN = b.ISBN,

								//WaitingGuid = d.WaitingGuid,

								RequestGuid = e.RequestGuid,
								Title = e.Title,
								Author = e.Author,
								//Comment = e.Comment,
								State = e.State,
								//ISBNRequest = e.ISBN,
								RequestDate = e.RequestDate,

								FiscalCode = c.FiscalCode,
								Name = c.Name,
								Surname = c.Surname,
								MobilePhone = c.MobilePhone,

								Email = f.Email
							};
			return rentsview.ToList();
		}

		public static IQueryable<RentRequestFinalView> ViewsLoader_Rents(LibraryContext ctx)
		{
            var rentsview = from x in ctx.Books
                            join a in ctx.Rents on x.BookGuid equals a.BookGuidFK
                            join b in ctx.ISBNLists on x.ISBNFK equals b.ISBN
                            join c in ctx.People on a.FiscalCodeFK equals c.FiscalCode
                            join f in ctx.Users on c.FiscalCode equals f.FiscalCode

                            // Creo un nuovo oggetto FinalView dove metto dentro tutti i risultati della query
                            select new RentRequestFinalView
                            {
                                BookGuid = x.BookGuid,
                                Available = x.Available,

                                ISBN = b.ISBN,

                                //WaitingGuid = d.WaitingGuid,
                                //RequestedOn = d.RequestedOn,
                                //ReceivedOn = d.ReceivedOn,

                                //RequestGuid = e.RequestGuid,
                                Title = x.Title,
                                //Author = e.Author,
                                //Comment = e.Comment,
                                //State = e.State,
                                //ISBNRequest = e.ISBN,
                                //RequestDate = e.RequestDate,

                                RentGuid = a.RentGuid,
                                WithdrawnOn = a.WithdrawnOn,
                                ReturnedOn = a.ReturnedOn,

                                FiscalCode = c.FiscalCode,
                                Name = c.Name,
                                Surname = c.Surname,
                                MobilePhone = c.MobilePhone,

                                Email = f.Email
                            };
            //Console.WriteLine(rentsview.Count());
            return rentsview;
		}

		public static IQueryable<UserRequestFinalView> ViewsLoader_UserRequest(LibraryContext ctx)
		{
			var view = from x in ctx.People
					   join a in ctx.Users on x.FiscalCode equals a.FiscalCode
					   join b in ctx.Requests on x.FiscalCode equals b.FiscalCodeFK
					   select new UserRequestFinalView
					   {
						   Email = a.Email,

						   FiscalCode = x.FiscalCode,
						   Name = x.Name,
						   Surname = x.Surname,
						   MobilePhone = x.MobilePhone,

						   RequestGuid = b.RequestGuid,
						   State = b.State,
						   Title = b.Title,
						   Author = b.Author,
						   RequestDate = b.RequestDate
					   };

			return view;
		}
		public static List<RentRequestFinalView> RentRequestFinalViewList(LibraryContext ctx)
		{
			List<RentRequestFinalView> a = new List<RentRequestFinalView>();
			foreach (var item in ViewsLoaders.ViewsLoader_Rents(ctx))
			{
				a.Add(item);
			}
			return a;
		}

		public static List<UserFinalView> UserFinalViewList(LibraryContext ctx)
		{
			List<UserFinalView> a = new List<UserFinalView>();
			foreach (var item in ViewsLoaders.ViewsLoader_Users(ctx))
			{
				a.Add(item);
			}
			return a;
		}

		public static List<BookFinalView> BookFinalViewList(LibraryContext ctx)
		{
			List<BookFinalView> a = new List<BookFinalView>();
			foreach (var item in ViewsLoaders.ViewsLoader_Books(ctx))
			{
				a.Add(item);
			}
			return a;
		}

		public static List<UserRequestFinalView> UserRequestFinalViewList(LibraryContext ctx) {
            List<UserRequestFinalView> a = new List<UserRequestFinalView>();
            foreach (var item in ViewsLoaders.ViewsLoader_UserRequest(ctx))
            {
                a.Add(item);
            }
            return a;
        }

		public static UserRequestFinalView getRequestById(Guid id, LibraryContext ctx)
		{
			foreach (var item in UserRequestFinalViewList(ctx))
				if (item.RequestGuid == id)
					return item;
			return null;
		}

		// Numero di libri comprati dalla biblioteca questo mese
		public static List<BookFinalView> MonthBookBought(LibraryContext ctx)
		{
			var thismonth = DateTime.Now.ToString("MM yyyy");
			var month = thismonth.Split(' ')[0];
			var year = thismonth.Split(' ')[1];

			List<BookFinalView> myList = new List<BookFinalView>();

			foreach (var item in BookFinalViewList(ctx))
			{
				var date = item.BuyDate.ToString().Split(' ')[0];
				if (date.Split('/')[1] == month && date.Split('/')[2] == year)
				{
					myList.Add(item);
				}
			}
			return myList;
		}

		public static IEnumerable<BookFinalView> getLastFiveInsertedBooks(LibraryContext ctx)
		{
			List<BookFinalView> sortedList = new List<BookFinalView>();
            foreach (var item in ViewsLoaders.ViewsLoader_Books(ctx))
            {
                sortedList.Add(item);
            }
            sortedList.Sort((x,y) => y.BuyDate.CompareTo(x.BuyDate));
			/*
			 * Se volessi ritornare una List formati da i primi 5 posso anche usare
			 * sortedList = sortedList.Take(5).ToList();
			 * ma le performance sarebbero assai peggiori
			*/

            return sortedList.Take(5); 
		}

		// Controllo dei dati di login e restituzione del tipo utente
		public static UserFinalView getUserType(string username, string password, LibraryContext ctx)
		{
			try
			{
				UserFinalView type = new UserFinalView();
				type.Type = String.Empty;
				foreach (var item in UserFinalViewList(ctx))
				{
					if (item.Username.ToString().ToLower() == username.ToString().ToLower() &&
						item.Password.ToString() == password.ToString())
					{
						type = item;
					}
				}
				return type;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		// Controlla se nel database è già presente un username, datone uno in ingresso
		public static bool checkUsername(string username, LibraryContext ctx)
		{
			bool check = false;
			foreach (var item in UserFinalViewList(ctx))
			{
				if (item.Username.ToLower() == username.ToLower())
				{
					check = true;
				}
			}
			return check;
		}
        public static UserFinalView getUserByFiscalCode(string FiscalCode, LibraryContext ctx)
        {
            UserFinalView user = new UserFinalView();
            foreach (var item in ViewsLoaders.UserFinalViewList(ctx))
            {
                if (item.FiscalCode == FiscalCode)
                {
                    user = item;
                    break;
                }
            }
            return user;
        }

		// Restituisce lista libri con un group for ISBN
		public static List<BookFinalView> BookGroupISBN2(LibraryContext ctx)
		{
			var pippo =  BookFinalViewList(ctx).GroupBy(x => x.ISBN).Select(y => y.FirstOrDefault());

			return pippo.ToList();
        }
		public static List<BookFinalView> BookGroupISBN(LibraryContext ctx)
		{

			List<BookFinalView> lista = BookFinalViewList(ctx);
            
				for (int a = 0; a < lista.Count; a++)
				{
                    if (CheckISBNList(lista[a].ISBN, lista, ctx) == true)
					{
					lista.Remove(lista[a]);
					}

                }
			return lista;
				
        }

		public static bool CheckISBNList(string ISBN, List<BookFinalView> pippo, LibraryContext ctx)
		{
			bool check = false;

			for (int i = 0; i < pippo.Count; i++)
			{
				if (pippo[i].ISBN == ISBN)
				{
					check = true;
				}
			}
			return check;

		}

        public static List<Request> FC_to_Request(string FiscalCode, LibraryContext ctx)
        {
            List<Request> a = ctx.Requests.Where(u => u.FiscalCodeFK == FiscalCode).ToList();
            return a;
        }

        public static List<Request> RequestList (LibraryContext ctx)
        {
			List <Request> a = ctx.Requests.ToList();
            return a;
        }
    }
}
