using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.WebEncoders.Testing;
using NationalLibrary.Data;
using NationalLibrary.FinalViews;
using System.Globalization;



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
                                CoverImg = x.CoverImg,
                                Room = a.Room,
                                Scaffhold = a.Schaffold,
                                Position = a.Position,
                                ISBN = i.ISBN

                            };

            return booksview;
        }

        public static IQueryable<RentRequestFinalView> ViewsLoader_Rents(LibraryContext ctx)
        {
            var rentsview = from x in ctx.Books
                            join a in ctx.Rents on x.BookGuid equals a.BookGuidFK
                            join b in ctx.ISBNLists on x.ISBNFK equals b.ISBN
                            join c in ctx.People on a.FiscalCodeFK equals c.FiscalCode
                            join d in ctx.WaitingLists on c.FiscalCode equals d.FiscalCodeFK
                            join e in ctx.Requests on c.FiscalCode equals e.FiscalCodeFK


                            // Creo un nuovo oggetto FinalView dove metto dentro tutti i risultati della query
                            select new RentRequestFinalView
                            {
                                BookGuid = x.BookGuid,
                                Available = x.Available,
                                ISBN = b.ISBN,

                                WaitingGuid = d.WaitingGuid,
                                RequestedOn = d.RequestedOn,
                                ReceivedOn = d.ReceivedOn,

                                RequestGuid = e.RequestGuid,
                                Title = e.Title,
                                Author = e.Author,
                                Comment = e.Comment,
                                State = e.State,
                                ISBNRequest = e.ISBN,
                                RequestDate = e.RequestDate,

                                RentGuid = a.RentGuid,
                                WithdrawnOn = a.WithdrawnOn,
                                ReturnedOn = a.ReturnedOn,
                            };

            return rentsview;
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

        // Controllo dei dati di login e restituzione del tipo utente
        public static UserFinalView getUserType (string username, string password, LibraryContext ctx)
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
            foreach(var item in UserFinalViewList(ctx))
            {
                if (item.Username.ToLower() == username.ToLower())
                {
                    check = true;
                }
            }
            return check;
        }
    }
}
