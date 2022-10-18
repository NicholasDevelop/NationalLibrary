using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NationalLibrary.Data
{
    public class Request
    {
        [Key]
        public Guid RequestGuid { get; set; }

        // Relation Person 1-N Request(FK)
        public string FiscalCodeFK { get; set; }
        [ForeignKey("FiscalCodeFK")]
        public Person Person { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        public string Comment { get; set; }
        [Required]
        public string State { get; set; }
        public string ISBN { get; set; }
        [Required]
        public DateTime RequestDate { get; set; }


    }
}
