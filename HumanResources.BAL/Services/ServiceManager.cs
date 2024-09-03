using HumanResources.BLL.Services.Contracts;

namespace HumanResources.BLL.Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly ICountryService _countryService;
        private readonly IDepartmentService _departmentService;
        private readonly IEmployeeService _employeeService;
        private readonly IJobHistoryService _jobHistoryService;
        private readonly IJobService _jobService;
        private readonly ILocationService _locationService;
        private readonly IRegionService _regionService;

        public ServiceManager(ICountryService countryService,
            IDepartmentService departmentService,
            IEmployeeService employeeService,
            IJobHistoryService jobHistoryService,
            IJobService jobService,
            ILocationService locationService,
            IRegionService regionService)
        {
            _countryService = countryService;
            _departmentService = departmentService;
            _employeeService = employeeService;
            _jobHistoryService = jobHistoryService;
            _jobService = jobService;
            _locationService = locationService;
            _regionService = regionService;
        }

        public ICountryService CountryService => _countryService;
        public IDepartmentService DepartmentService => _departmentService;
        public IEmployeeService EmployeeService => _employeeService;
        public IJobHistoryService JobHistoryService => _jobHistoryService;
        public IJobService JobService => _jobService;
        public ILocationService LocationService => _locationService;
        public IRegionService RegionService => _regionService;
    }
}
