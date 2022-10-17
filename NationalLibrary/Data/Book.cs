using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.ComponentModel.DataAnnotations;

namespace NationalLibrary.Data
{
    public class Book
    {
        [Key]
        public Guid BookGuid { get; set; }

        [Required]
        public string Title { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        public string PublishingHouse { get; set; }
        public Boolean Available { get; set; }
        [Required]
        public string Presentation { get; set; }
        [Required]
        public string Genre { get; set; }
        [Required]
        public string CoverImg { get; set; }
        [Required]
        public DateTime BuyDate { get; set; }
        [Required]
        public string Price { get; set; }


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
