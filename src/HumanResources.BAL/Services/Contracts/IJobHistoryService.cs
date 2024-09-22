using HumanResources.DTO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources.BLL.Services.Contracts
{
    public interface IJobHistoryService
    {
        IEnumerable<JobHistory> GetAllJobHistory();
        IEnumerable<JobHistory> GetJobHistoryById(int employeeId);
        JobHistory? GetJobHistoryById(int employeeId, DateTime startDate);
        bool AddJobHistory(JobHistory jobHistory);
        bool UpdateJobHistory(JobHistory jobHistory);
        bool DeleteJobHistory(int employeeId);
        bool DeleteJobHistory(int employeeId, DateTime startDate);
    }
}
