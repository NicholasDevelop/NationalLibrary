using System.Globalization;

namespace NationalLibrary.FinalViews
{
    public class UserFinalView
    {
        // Person
        public string FiscalCode { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string MobilePhone { get; set; }
        public DateTime BirthDate { get; set; }

        // Residence
        public string City { get; set; }
        public string Street { get; set; }
        public int? CAP { get; set; }
        public string Province { get; set; }

        // Document
        public string DocumentNumber { get; set; }
        public string DocumentType { get; set; }
        public string ReleasedBy { get; set; }
        public DateTime ExpiredOn { get; set; }

        // User
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public DateTime? RegisterDate { get; set; }


    }
}
