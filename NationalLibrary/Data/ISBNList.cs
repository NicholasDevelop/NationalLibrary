using System.ComponentModel.DataAnnotations;

namespace NationalLibrary.Data
{
    public class ISBNList
    {

        [Key]
        public string ISBN { get; set; }

        // Relation ISBNList 1-N WaitingList(FK)
        public List<WaitingList> WaitingLists { get; set; }

        // Relation ISBNList 1-N Book(FK)
        public List<Book> Books { get; set; }
    }
}
