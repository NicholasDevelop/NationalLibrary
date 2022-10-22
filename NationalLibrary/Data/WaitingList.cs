using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NationalLibrary.Data
{
    public class WaitingList
    {
        [Key]
        public Guid WaitingGuid { get; set; }

        // Relation User 1-N Waiting List(FK)
        [Required]
        public string FiscalCodeFK { get; set; }
        [ForeignKey("FiscalCodeFK")]
        public Person Person { get; set; }

        public DateTime RequestedOn { get; set; }
        public DateTime? ReceivedOn { get; set; }
        public bool IsFromRequest { get; set; }


        // Relation WaitingList(FK) N-1 ISBNList
        [Required]
        public string ISBNFK { get; set; }
        [ForeignKey("ISBNFK")]
        public ISBNList ISBNList { get; set; }
    }
}
