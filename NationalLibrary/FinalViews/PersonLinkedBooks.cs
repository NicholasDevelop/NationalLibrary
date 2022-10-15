namespace NationalLibrary.FinalViews
{
    public class PersonLinkedBooks
    {
        string FiscalCode { get; set; }
        string Name { get; set; }
        string Surname { get; set; }
        string MobilePhone { get; set; }
        Guid BookGuid { get; set; }
        DateTime WithdrawnOn { get; set; }
        DateTime? ReturnedOn { get; set; }

    }
}
