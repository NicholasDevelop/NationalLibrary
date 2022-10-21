namespace NationalLibrary.FinalViews
{
    public class WaitingBookStatusFinalView
    {
        public WaitingBookStatusFinalView(string name, string title)
        {
            Name = name;
            Title = title;
        }

        public string Name { get; set; }
        public string Title { get; set; }
    }
}
