﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NationalLibrary.Data
{
	public class Person
	{
		private string name;

		[Key]
		public string FiscalCode { get; set; }
        public string Type { get; set; }
        public string Name { get => name; set { name = DataController.CheckStrings(value, "Nome"); } }
		public string Surname { get; set; }
		public string MobilePhone { get; set; }
		public DateTime Birthday { get; set; }

        public string FCRelatedTO { get; set; }

		// Relation Residence(FK) 1-N person
        public Guid AddressGuidFK { get; set; }
        [ForeignKey("AddressGuidFK")]
        public Residence Residence { get; set; }

        // Relation Document 1-1 Person(FK)
        public string DocumentNumberFK { get; set; }
		public Document Document { get; set; }

		// Relation Person 1-1 User(FK)
		public User User { get; set; }

        // Relation Person 1-N Rent(FK)
        public List<Rent> Rents { get; set; }

        // Relation Person 1-N Waiting List (FK)
        public List<WaitingList> WaitingLists { get; set; }
    }
}