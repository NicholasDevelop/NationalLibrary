namespace NationalLibrary.Data
{
    public class Request
    {

        Guid RequestGuid { get; set; }
        string FiscalCode { get; set; }
        string Title { get; set; }
        string Author { get; set; }
        string Comment { get; set; }
        string? ISBN { get; set; }
        DateTime RequestDate { get; set; }


    }
}
