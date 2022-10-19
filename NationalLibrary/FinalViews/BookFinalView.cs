namespace NationalLibrary.FinalViews
{
    public class BookFinalView
    {

        public Guid BookGuid { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string PublishingHouse { get; set; }
        public bool Available { get; set; }
        public string Presentation { get; set; }
        public string Genre { get; set; }
        public byte[] CoverImg { get; set; }
        public string Price { get; set; }
        public DateTime BuyDate { get; set; }
        public string Room { get; set; }
        public string Scaffhold { get; set; }
        public string Shelf { get; set; }
        public int? Position { get; set; }
        public string ISBN { get; set; }

    }
}
