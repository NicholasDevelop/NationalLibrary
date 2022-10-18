using System.ComponentModel.DataAnnotations;

namespace NationalLibrary.Data
{
    public class ISBNList
    {
        public Guid WaitingGuid { get; set; }
        public WaitingList WaitingList { get; set; }
        
        public Guid BookGuid { get; set; }
        public Book Book { get; set; }

        [Required]
        public string ISBN { get; set; }
    }
}
