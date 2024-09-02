using WebApi.Controllers;
using WebApi.Models;

namespace WebApi.Repositories
{
    public interface IJobHistoryRepository : IBaseRepository<JobHistory, Int32>
    {
        JobHistory Get(int employeeId, DateTime startDate);
        IEnumerable<JobHistory> GetEmployeeHistory(int employeeId);
        bool Delete(int employeeId, DateTime? startDate);
    }
}
