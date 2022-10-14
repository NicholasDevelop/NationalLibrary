using System.Globalization;

namespace NationalLibrary.Data
{
    
    public class UserFinalView
    {
      public string FiscalCode { get; set; }
      public string Type { get; set; }
      public string Name { get; set; }
      public string Surname { get; set; }
      public string Email { get; set; }
      public string City { get; set; }
      public string Street { get; set; }
      public int CAP { get; set; }
      public string Province { get; set; }
      public string DocumentNumber { get; set; }
      public string DocumentType { get; set; }
      public string ReleasedBy { get; set; }
      public DateTime ExpiredOn { get; set; }
      public string Username { get; set; }
      public string UserType { get; set; }
      public string Password { get; set; }


    }
}
