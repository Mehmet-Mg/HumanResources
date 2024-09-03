using HumanResources.DTO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources.BLL.Services.Contracts
{
    public interface IJobService
    {
        IEnumerable<Job> GetAllJob();
        Job? GetJobById(string jobId);
        bool AddJob(Job job);
        bool UpdateJob(Job job);
        bool DeleteJob(string jobId);
    }
}
