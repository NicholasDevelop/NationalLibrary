using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NationalLibrary.Data
{
    public class User
    {
        [Key]
        // Relation Person 1-1 User(FK-PK)
        public string FiscalCode { get; set; }
        public Person Person { get; set; }
        
        public string Username { get; set; }
        public string Password { get; set; }
        public string UserType { get; set; }


        // Relation User 1-N Rent(FK)
        public List<Rent> Rents { get; set; }

        // Relation User 1-N Waiting List (FK)
        public List<WaitingList> WaitingLists { get; set; }
    }
}
