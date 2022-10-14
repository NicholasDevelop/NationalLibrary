using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NationalLibrary.Data
{
    public class Rent
    {
        [Key]
        public Guid RentGuid { get; set; }

        public Guid BookGuid { get; set; }

        // Relation User 1-N Rent(FK)
        public string FiscalCodeFK { get; set; }
        [ForeignKey("FiscalCodeFK")]
        public User User { get; set; }

        // Relation Book 1-1 Rent(FK)
        public Guid BookGuid { get; set; }
        public Book Book { get; set; }




    }
}
