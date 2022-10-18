using System.ComponentModel.DataAnnotations.Schema;

namespace NationalLibrary.Data
{
    public class Request
    {

        Guid RequestGuid { get; set; }

        // Relation Person 1-N Request(FK)
        string FiscalCodeFK { get; set; }
        [ForeignKey("FiscalCodeFK")]
        public Person Person { get; set; }

        string Title { get; set; }
        string Author { get; set; }
        string Comment { get; set; }
        string? ISBN { get; set; }
        DateTime RequestDate { get; set; }


    }
}
