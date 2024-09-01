using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class Country
    {
        [StringLength(2)]
        public string CountryId { get; set; }
        [StringLength(60)]
        public string? CountryName { get; set; }
        public int? RegionId { get; set; }
    }
}
