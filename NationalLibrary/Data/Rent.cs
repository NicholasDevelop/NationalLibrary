using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NationalLibrary.Data
{
    public class Rent
    {
        [Key]
        public Guid RentGuid { get; set; }

        // Relation User 1-N Rent(FK)
        [Required]
        public string FiscalCodeFK { get; set; }
        [ForeignKey("FiscalCodeFK")]
        public Person Person { get; set; }

        // Relation Book 1-N Rent(FK)
        public Guid BookGuidFK { get; set; }
        [ForeignKey("BookGuidFK")]
        public Book Book { get; set; }

        public DateTime WithdrawnOn { get; set; }
        public DateTime? ReturnedOn { get; set; }

    }
}
