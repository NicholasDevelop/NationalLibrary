using System.ComponentModel.DataAnnotations;

namespace NationalLibrary.Data
{
	public class Location
	{
		#region Variables
		
		private string scaffhold;
		private string shelf;
		private int? position;
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
		public string Room { get => room; set { room = value; } }
		public string Schaffold { get => scaffhold; set { scaffhold = value; } }
		public string Shelf { get => shelf; set { shelf = value; } }
		public int? Position { get => position; set { position = value; } }
		#endregion

		// Relation Location 1-1 Book(FK)
		public Book Book { get; set; }
	}
}
