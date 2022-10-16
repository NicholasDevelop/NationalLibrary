using System.ComponentModel.DataAnnotations;

namespace NationalLibrary.Data
{
    public class Residence
    {
        [Key]
        public Guid AddressGuid { get; set; }
        public string City { get ;set;}  
        public string Street { get; set; }
        public int? CAP { get; set; }
        public string Province { get; set; }
        
        // Relation Residence 1-N Person
        public List<Person> People { get; set; }
    }
}
