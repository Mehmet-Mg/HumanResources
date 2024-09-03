using HumanResources.BLL.Services.Contracts;
using HumanResources.DAL.Repositories;
using HumanResources.DTO.Models;

namespace HumanResources.BLL.Services
{
    public class JobManager : IJobService
    {
        private readonly IJobRepository _jobRepository;

        public JobManager(IJobRepository jobRepository)
        {
            _jobRepository = jobRepository;
        }

        public bool AddJob(Job job)
        {
            return _jobRepository.Add(job);
        }

        public bool DeleteJob(string jobId)
        {
            return _jobRepository.Delete(jobId);
        }

        public IEnumerable<Job> GetAllJob()
        {
            return _jobRepository.GetAll();
        }

        public Job? GetJobById(string jobId)
        {
            return _jobRepository.Get(jobId);
        }

        public bool UpdateJob(Job job)
        {
            return _jobRepository.Update(job);
        }
    }
}
