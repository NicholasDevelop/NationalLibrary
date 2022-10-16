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

        public DateTime RequiredOn { get; set; }


        // Relation Book N-N WaitiList
        //public ICollection<Book> Books { get; set; }
        public List<WaitingList_Book> WaitingList_Books { get; set; }
    }
}
