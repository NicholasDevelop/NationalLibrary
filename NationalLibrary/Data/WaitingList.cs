using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NationalLibrary.Data
{
    public class WaitingList
    {
        [Key]
        public Guid WaitingGuid { get; set; }

        // Relation User 1-N Waiting List(FK)
        public string FiscalCodeFK { get; set; }
        [ForeignKey("FiscalCodeFK")]
        public User User { get; set; }

        // Relation Book 1-1 WaitiList(FK)
        public string ISBN { get; set; }
        public Book Book { get; set; }

        public DateTime RequiredOn { get; set; }


    }
}
