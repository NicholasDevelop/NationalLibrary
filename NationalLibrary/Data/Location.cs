using System.ComponentModel.DataAnnotations;

namespace NationalLibrary.Data
{
	public class Location
	{
		#region Variables
		
		[Required]
		private string scaffhold;
		[Required]
		private string shelf;
		[Required]
		private int position;
		[Required]
		private string room;
		#endregion

		#region Constructors
		public Location()
		{
			
		}
		public Location(Guid locationGuid, string room, string schaffold, string shelf, int position, Book book)
		{
			LocationGuid = locationGuid;
			Room = room;
			Schaffold = schaffold;
			Shelf = shelf;
			Position = position;
			Book = book;
		}
        #endregion

        #region Properties
        [Key]
        public Guid LocationGuid { get; set; }
		public string Room { get => room; set { room = DataController.CheckStrings(value, "stanza"); } }
		public string Schaffold { get => scaffhold; set { scaffhold = DataController.CheckScaffholdOrShelf(value, "scaffale"); } }
		public string Shelf { get => shelf; set { shelf = DataController.CheckScaffholdOrShelf(value, "ripiano"); } }
		public int Position { get => position; set { position = value; } }
		#endregion

		// Relation Location 1-1 Book(FK)
		public Book Book { get; set; }
	}
}
