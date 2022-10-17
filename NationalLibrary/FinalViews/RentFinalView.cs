namespace NationalLibrary.FinalViews
{
    public class RentFinalView
    {
        public Guid? RentGuid { get; set; }
        public Guid BookGuid { get; set; }
        public string Title { get; set; }
        public byte[] CoverImg { get; set; }
        public string FiscalCode { get; set; }
        public DateTime? RequiredOn { get; set; }
        public DateTime? WithdrawOn { get; set; }
        public DateTime? ReturnedOn { get; set; }
        public string ISBN { get; set; }
    }
}
