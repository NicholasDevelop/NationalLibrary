using System.ComponentModel.DataAnnotations;

namespace NationalLibrary.Data
{
	public class Document
	{
		#region Variables
		private string documentNumber;
		private string documentType;
		private string releasedBy;
		private DateTime expireOn;
		#endregion

		public Document(string documentNumber, string documentType, string releasedBy, DateTime expireOn)
		{
			DocumentNumber = documentNumber;
			DocumentType = documentType;
			ReleasedBy = releasedBy;
			ExpireOn = expireOn;
		}

        #region Properties
        [Key]
        public string DocumentNumber { get => documentNumber; set { documentNumber = DataController.CheckDocumentNumber(value); } }
        [Required]
        public string DocumentType { get => documentType; set => documentType = value; }
        [Required]
        public string ReleasedBy { get => releasedBy; set { releasedBy = DataController.CheckStrings(value, "autore"); } }
        [Required]
        public DateTime ExpireOn { get => expireOn; set => expireOn = value; }
		#endregion

		// Document 1-1 Person
		public Person Person { get; set; }
	}
}
