using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        public byte[] CoverImg { get; set; }
        [Required]
        public DateTime BuyDate { get; set; }
        [Required]
        public string Price { get; set; }


        // Relation Book 1-N Rent(FK)
        public List<Rent> Rents { get; set; }

        // Relation Location 1-1 Book(FK)
        public Guid LocationGuidFK { get; set; }
        public Location Location { get; set; }


        // Relation ISBNList 1-N Book(FK)
        public string ISBNFK { get; set; }
        [ForeignKey("ISBNFK")]
        public ISBNList ISBNList { get; set; }

    }
}
