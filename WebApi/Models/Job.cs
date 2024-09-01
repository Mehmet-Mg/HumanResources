using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class Job
    {
        [StringLength(10)]
        public string JobId { get; set; }
        [StringLength(35)]
        public string JobTitle { get; set; }
        public decimal? MinSalary { get; set; }
        public decimal? MaxSalary { get; set; }
    }
}
