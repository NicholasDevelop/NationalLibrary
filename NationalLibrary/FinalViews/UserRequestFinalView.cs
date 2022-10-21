using System.ComponentModel.DataAnnotations;

namespace NationalLibrary.FinalViews
{
    public class UserRequestFinalView
    {
        // User
        public string Email { get; set; }

        // People
        public string FiscalCode { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string MobilePhone { get; set; }

        // Request
        public Guid RequestGuid { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string State { get; set; }
        public DateTime RequestDate { get; set; }

    }
}
