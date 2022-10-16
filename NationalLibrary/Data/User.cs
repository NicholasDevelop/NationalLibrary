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
        public string Email { get; set; }

    }
}
