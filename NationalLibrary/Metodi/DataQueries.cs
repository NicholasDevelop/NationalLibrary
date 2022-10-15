using NationalLibrary.FinalViews;

namespace NationalLibrary.Metodi
{
    public class DataQueries
    {
        public static LibraryContext ctx = new LibraryContext();
        /// <summary>
        /// Ritorna un join di Person e Rent dentro un oggetto PersonLinkedBooks contenente (FiscalCode, Name, Surname, MobilePhone, BookGuid, WithdrawnOn, ReturnedOn?)
        /// </summary>
        /// <returns></returns>
        public IQueryable<PersonLinkedBooks> PersonLinkedBooks()
        {
          

            var personlinkedbooks =  from x in ctx.People  
                                     join a in ctx.Rents on x.FiscalCode equals a.FiscalCode //questo va a posto quando mattia sistema relazioni


                            // Creo un nuovo oggetto FinalView dove metto dentro tutti i risultati della query
                            select new UserFinalView
                            {
                                FiscalCode = x.FiscalCode,
                                Name = x.Name,
                                Surname = x.Surname,
                                MobilePhone = x.MobilePhone,
                                BookGuid = a.BookGuid,      //questo va a posto quando mattia sistema relazioni
                                WithdrawnOn = a.WithdrawnOn,//questo va a posto quando mattia sistema relazioni
                                ReturnedOn = a.ReturnedOn   //questo va a posto quando mattia sistema relazioni

                            };
            return personlinkedbooks;

        }

    }
}
