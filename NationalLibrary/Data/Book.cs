using System.ComponentModel.DataAnnotations;

namespace NationalLibrary.Data
{
    public class Book
    {
        [Key]
        public Guid BookGuid { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string PublishingHouse { get; set; }
        public Boolean Avaiable { get; set; }

        public string Presentation { get; set; }
        public string Genre { get; set; }
        public string CoverImg { get; set; }

        // Relation Book 1-1 Rent(FK)
        public Rent Rent { get; set; }

        // Relation Book 1-1 WaitingList(FK)
        public WaitingList WaitingList { get; set; }

        // Relation Book 1-N Rent(FK)
        public List<Rent> Rents { get; set; }


        // Relation Location 1-1 Book(FK)
        public Guid LocationGuidFK { get; set; }
        public Location Location { get; set; }


        // Relation Book N-N WaitingList
        //public ICollection<WaitingList> WaitingLists { get; set; }
        public List<WaitingList_Book> WaitingList_Books { get; set; }

    }
}
