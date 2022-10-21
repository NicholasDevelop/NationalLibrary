namespace NationalLibrary.FinalViews
{
    public class RentRequestFinalView
    {
        // Book
        public Guid BookGuid { get; set; }
        public bool Available { get; set; } 

        // ISBNList
        public string ISBN { get; set; }

        // WaitingList
        public Guid WaitingGuid { get; set; }
        public DateTime RequestedOn { get; set; }
        public DateTime? ReceivedOn { get; set; }

        // Request
        public Guid RequestGuid { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Comment { get; set; }
        public string State { get; set; }
        public string ISBNRequest { get; set; }
        public DateTime RequestDate { get; set; }

        // Rent
        public Guid RentGuid { get; set; }
        public DateTime? WithdrawnOn { get; set; }
        public DateTime? ReturnedOn { get; set; }

        // Person
        public string FiscalCode { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string MobilePhone { get; set; }

        // User
        public string Email { get; set; }

    }
}
