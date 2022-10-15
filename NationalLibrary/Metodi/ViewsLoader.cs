using NationalLibrary.FinalViews;
using System.Globalization;


namespace NationalLibrary.Metodi
{


    public class ViewsLoaders
    {
        public static LibraryContext ctx = new LibraryContext();

        // Ritorna tutti i dati di una persona collegandi (Join di User, Person, Residence, Document)
        /// <summary>
        /// Carica tutti i dati di un utente facendo join di Person, User, Residence e Document
        /// </summary>
        /// <returns></returns>
        public static IQueryable<UserFinalView> Users()
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
                           MobilePhone = x.MobilePhone
                           Birthday = x.Birthday,
                           Email = x.Email,
                           City = b.City,
                           Street = b.Street,
                           CAP = b.CAP,
                           Province = b.Province,
                           DocumentNumber = c.DocumentNumber,
                           DocumentType = c.DocumentType,
                           ReleasedBy = c.ReleasedBy,
                           ExpiredOn = c.ExpireOn,
                           Username = a.Username,
                           UserType = a.UserType,
                           Password = a.Password

                       };

            return usersview;
        }

        // Ritorna tutti i dati di un libro collegandoli (Join di Book e Location)
        /// <summary>
        /// Ritorna tutti i dati di un libro facendo un join di Book e Location
        /// </summary>
        /// <returns></returns>
        public static IQueryable<BookFinalView> Books()
        {
            var booksview = from x in ctx.Books  
                            join a in ctx.Locations on x.LocationGuidFK equals a.LocationGuid


                            // Creo un nuovo oggetto BookFinalView dove metto dentro tutti i risultati della query
                            select new BookFinalView
                            {
                                BookGuid = x.BookGuid,
                                Title = x.Title,
                                ISBN = x.ISBN,
                                Author = x.Author,
                                PublishingHouse = x.PublishingHouse,
                                OwnedCopies = x.OwnedCopies,
                                AvaiableCopies = x.AvaiableCopies,
                                Genre = x.Genre,
                                Room = a.Room,
                                Scaffhold = a.scaffhold,
                                Shelf = a.Shelf,
                                Position = a.Position

                            };

            return booksview;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static IQueryable<BookFinalView> Avaiability()



        }




    
}
