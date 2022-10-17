﻿using NationalLibrary.Data;
using NationalLibrary.FinalViews;
using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;

namespace NationalLibrary.Metodi
{
    public class DataQueries
    {
//        private static readonly LibraryContext ctx;
        


        //public static List<RentFinalView> RentFinalViewList()
        //{
        //    List<RentFinalView> a = new List<RentFinalView>();
        //    foreach (var item in ViewsLoaders.ViewsLoader_Rents())
        //    {
        //        if (item.Title == "stronzo")
        //        {
        //            a.Add(item);
        //        }
        //    }
        //    return a;
        //}

     
        //////////////////        LISTE COMPLETE         \\\\\\\\\\\\\\\\\\\\\
        public static List<RentFinalView> RentFinalViewList()
        {
            List<RentFinalView> a = new List<RentFinalView>();
            foreach (var item in ViewsLoaders.ViewsLoader_Rents())
            {
                a.Add(item);
            }
            return a;
        }

        public static List<UserFinalView> UserFinalViewList()
        {
            List<UserFinalView> a = new List<UserFinalView>();
            foreach (var item in ViewsLoaders.ViewsLoader_Users())
            {
                a.Add(item);
            }
            return a;
        }

        public static List<BookFinalView> BookFinalViewList()
        {
            List<BookFinalView> a = new List<BookFinalView>();
            foreach (var item in ViewsLoaders.ViewsLoader_Books())
            {
                a.Add(item);
            }
            return a;
        }

        // QUERY DI RICERCA






        //////////////////  QUERY MANIPOLAZIONE UTENTI   \\\\\\\\\\\\\\\\\\\\\
        public static void InsertUser
        (
            string FiscalCode, string Type, string Name, string Surname, string MobilePhone, DateTime BirthDate,
            string City, string Street, int CAP, string Province,
            string DocumentNumber, string DocumentType, string ReleasedBy, DateTime ExpireOn,
            string Email, string Username, string Password, string FCRelatedTO, LibraryContext ctx
        )

        {
           // ctx = new LibraryContext();
            var newuser            = new Person() { FiscalCode = FiscalCode, Name = Name, Surname = Surname,Type = Type, MobilePhone = MobilePhone, BirthDate = BirthDate, FCRelatedTO = FCRelatedTO, SignUpDate = DateTime.Now};
                newuser.Residence  = new Residence() { City = City, Street = Street, CAP = CAP, Province = Province, AddressGuid = Guid.NewGuid() };
                newuser.Document   = new Document() { DocumentNumber = DocumentNumber, DocumentType = DocumentType, ReleasedBy = ReleasedBy, ExpireOn = ExpireOn};
                newuser.User       = new User() {Email = Email, Username = Username, Password = Password};

            //Trace.WriteLine(newuser);
            //Trace.WriteLine(newuser.Residence);
            // DA TESTARE !!!
            ctx.People.Add(newuser);
            ctx.Users.Add(newuser.User);
            ctx.Documents.Add(newuser.Document);
            ctx.Residences.Add(newuser.Residence);
            ctx.SaveChanges();
        }

        public static void InsertTutorUser
        (
            string FiscalCode, string Type, string Name, string Surname, string MobilePhone, DateTime BirthDate,
            string DocumentNumber,
            string FCRelatedTO, LibraryContext ctx
        )

        {
            var newuser = new Person() { FiscalCode = FiscalCode, Name = Name, Surname = Surname, MobilePhone = MobilePhone, BirthDate = BirthDate,SignUpDate = DateTime.Now, FCRelatedTO = FCRelatedTO };
            newuser.Document = new Document() { DocumentNumber = DocumentNumber };

            // DA TESTARE !!!
            ctx.People.Add(newuser);
            ctx.SaveChanges();
        }
        public static void DeleteUser(string FiscalCode, LibraryContext ctx)
        {
            //Seleziono tutti quelli che ci sono nei contatti e la trasformo nella lista

            User   view2 = ctx.Users.Where (u => u.FiscalCode == FiscalCode).ToList()[0];
            Person view = ctx.People.Where(u => u.FiscalCode == FiscalCode).ToList()[0];
            ctx.Users.Remove(view2);
            ctx.People.Remove(view);

            ctx.SaveChanges();

        }

        public static void EditUser
        (
            string FiscalCode, string Type, string Name, string Surname, string MobilePhone, DateTime BirthDate, string FCRelatedTo,
            string City, string Street, int CAP, string Province,
            string DocumentNumber, string DocumentType, string ReleasedBy, DateTime ExpireOn,
            string Email, string Username, string Password, LibraryContext ctx
        )
        {

            Person          a = ctx.People.Where(u => u.FiscalCode == FiscalCode).ToList()[0];
            List<Residence> b = ctx.Residences.Where(u => a.AddressGuidFK == u.AddressGuid).ToList();
            List<Document>  c = ctx.Documents.Where(u => a.DocumentNumberFK == u.DocumentNumber).ToList();
            List<User>      d = ctx.Users.Where(u => a.FiscalCode == u.FiscalCode).ToList();

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

        //////////////////  QUERY MANIPOLAZIONE LIBRI   \\\\\\\\\\\\\\\\\\\\\

        public static void InsertBook
        (
            Guid BookGuid, string Title, string Author, string PublishingHouse, bool Available, string Presentation,string Genre, string Coverimg,
            DateTime BuyDate, string Price,

            string Room, string Scaffhold, int Position, string Shelf, LibraryContext ctx
        )

        {
            var newbook =           new Book() { BookGuid = Guid.NewGuid(), Title = Title, Author = Author, PublishingHouse = PublishingHouse, Available = Available, Presentation = Presentation, Genre = Genre, CoverImg = Coverimg,BuyDate = DateTime.Now, Price = Price };
                newbook.Location =  new Location() { Room = Room, Schaffold = Scaffhold, Shelf = Shelf, Position = Position };


            ctx.Books.Add(newbook);
            ctx.Locations.Add(newbook.Location);
            ctx.SaveChanges();
        }

        public static void DeleteBook(Guid BookGuid, LibraryContext ctx)
        {
            //Seleziono tutti quelli che ci sono nei contatti e la trasformo nella lista

            Book    view = ctx.Books.Where(u => u.BookGuid == BookGuid).ToList()[0];
            ctx.Books.Remove(view);

            ctx.SaveChanges();

        }

        public static void EditBook
        (
            Guid BookGuid, string Title, string Author, string PublishingHouse, bool Available, string Presentation, string Genre,
            string Coverimg, string Room, string Scaffhold, string Shelf, int Position,
            LibraryContext ctx
        )
        {

            Book            a = ctx.Books.Where(u => u.BookGuid == BookGuid).ToList()[0];
            List<Location>  b = ctx.Locations.Where(u => u.LocationGuid == a.LocationGuidFK).ToList();

            a.Title = Title;
            a.Author = Author;
            a.PublishingHouse = PublishingHouse;
            a.Available = Available;
            a.Presentation = Presentation;
            a.Genre = Genre;
            a.CoverImg = Coverimg;
            b[0].Room = Room;
            b[0].Schaffold = Scaffhold;
            b[0].Shelf = Shelf;
            b[0].Position = Position;

            ctx.SaveChanges();




        }






    }

}