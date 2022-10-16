namespace NationalLibrary.FinalViews
{
    public class BookFinalView
    {

        Guid BookGuid { get; set; }
        string Title { get; set; }
        string Author { get; set; }
        string PublishingHouse { get; set; }
        bool Avaiable { get; set; }
        string Presentation { get; set; }
        string Genre { get; set; }
        string CoverImg { get; set; }
        string Room { get; set; }
        char Scaffhold { get; set; }
        char Shelf { get; set; }
        int Position { get; set; }
    }
}
