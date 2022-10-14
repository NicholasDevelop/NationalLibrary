using NationalLibrary.Data;
using System.Globalization;


namespace NationalLibrary.Metodi
{
    

    public class ViewsLoaders
    {
        public static LibraryContext ctx = new LibraryContext();


        public static IQueryable<UserFinalView> ViewsLoader_Users()
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
            
            
            
        
        }




    
}
