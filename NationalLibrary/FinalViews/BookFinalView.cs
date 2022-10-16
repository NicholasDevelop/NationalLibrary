namespace NationalLibrary.FinalViews
{
    public class BookFinalView
    {

        Guid BookGuid { get; set; }
        string Title { get; set; }
        string ISBN { get; set; }
        string Author { get; set; }
        string PublishingHouse { get; set; }
        string OwnedCopies { get; set; }
        string AvaiableCopies { get; set; }
        string Presentation { get; set; }
        string Genre { get; set; }
        string Room { get; set; }
        char Scaffhold { get; set; }
        char Shelf { get; set; }
        int Position { get; set; }
    }
}
