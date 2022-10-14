using System.ComponentModel.DataAnnotations;

namespace NationalLibrary.Data
{
    public class Book
    {
        [Key]
        public Guid BookGuid { get; set; }
        public string Title { get; set; }
        public string ISBN { get; set; }
        public string Author { get; set; }
        public string PublishingHouse { get; set; }
        public int OwnedCopies { get; set; }
        public int AvaiableCopies { get; set; }
        public string Presentation { get; set; }
        public string Genre { get; set; }

        // Relation Book 1-1 Rent(FK)
        public Rent Rent { get; get; }

        // Relation Book 1-1 WaitingList(FK)
        public WaitingList WaitingList { get; set; }

        // Relation Location 1-1 Book(FK)
        public GUid LocationGuidFK { get; set; }
        public Location Location { get; set; }

    }
}
