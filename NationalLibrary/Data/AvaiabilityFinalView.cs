namespace NationalLibrary.Data
{
    public class AvaiabilityFinalView
    {
        public Guid BookGuid { get; set; }
        public string Title { get; set; }
        public string ISBN { get; set; }
        public string Author { get; set; }
        public string PublishingHouse { get; set; }
        public int OwnedCopies { get; set; }
        public int AvaiableCopies { get; set; }
        public string Presentation { get; set; }
        public string Genre { get; set; }
        public string Room { get; set; }
        
    }
}
