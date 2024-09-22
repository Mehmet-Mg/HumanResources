using System.ComponentModel.DataAnnotations;

namespace HumanResources.DTO.Models
{
    public class Location
    {
        public int LocationId { get; set; }
        [StringLength(40)]
        public string? StreetAddress { get; set; }
        [StringLength(12)]
        public string? PostalCode { get; set; }
        [StringLength(30)]
        public string City { get; set; }
        [StringLength(25)]
        public string? StateProvince { get; set; }
        
        [StringLength(2)]
        public string? CountryId { get; set; }
    }
}
