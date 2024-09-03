using HumanResources.BLL.Services.Contracts;
using HumanResources.DAL.Repositories;
using HumanResources.DTO.Models;

namespace HumanResources.BLL.Services
{
    public class JobHistoryManager : IJobHistoryService
    {
        private readonly IJobHistoryRepository _jobHistoryRepository;

        public JobHistoryManager(IJobHistoryRepository jobHistoryRepository)
        {
            _jobHistoryRepository = jobHistoryRepository;
        }

        public bool AddJobHistory(JobHistory jobHistory)
        {
            return _jobHistoryRepository.Add(jobHistory);
        }

        public bool DeleteJobHistory(int employeeId)
        {
            return _jobHistoryRepository.Delete(employeeId);
        }

        public bool DeleteJobHistory(int employeeId, DateTime startDate)
        {
            return _jobHistoryRepository.Delete(employeeId, startDate);
        }

        public IEnumerable<JobHistory> GetAllJobHistory()
        {
            return _jobHistoryRepository.GetAll();
        }

        public IEnumerable<JobHistory> GetJobHistoryById(int employeeId)
        {
            return _jobHistoryRepository.GetEmployeeHistory(employeeId);
        }

        public JobHistory? GetJobHistoryById(int employeeId, DateTime startDate)
        {
            return _jobHistoryRepository.Get(employeeId, startDate);
        }

        public bool UpdateJobHistory(JobHistory jobHistory)
        {
            return _jobHistoryRepository.Update(jobHistory);
        }
    }
}
