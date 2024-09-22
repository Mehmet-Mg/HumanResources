using System.ComponentModel.DataAnnotations;

namespace HumanResources.DTO.Models
{
    public class JobHistory
    {
        public int EmployeeId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        [StringLength(10)]
        public string JobId { get; set; }
        public int? DepartmentId { get; set; }
    }
}
