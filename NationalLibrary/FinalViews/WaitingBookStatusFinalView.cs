namespace NationalLibrary.FinalViews
{
    public class WaitingBookStatusFinalView
    {
        public WaitingBookStatusFinalView(string name, string title)
        {
            Name = name;
            Title = title;
        }

        string Name { get; set; }
        string Title { get; set; }
    }
}
