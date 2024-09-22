using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources.BLL.Services.Contracts
{
    public interface IServiceManager
    {
        ICountryService CountryService { get; }
        IDepartmentService DepartmentService { get; }
        IEmployeeService EmployeeService { get; }
        IJobHistoryService JobHistoryService { get; }
        IJobService JobService { get; }
        ILocationService LocationService { get; }
        IRegionService RegionService { get; }

    }
}
