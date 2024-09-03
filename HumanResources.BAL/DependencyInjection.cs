using HumanResources.BLL.Services;
using HumanResources.BLL.Services.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace HumanResources.BLL
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddBusinessLogicLayer(this IServiceCollection services)
        {
            services.AddTransient<ICountryService, CountryManager>();
            services.AddTransient<IDepartmentService, DepartmentManager>();
            services.AddTransient<IEmployeeService, EmployeeManager>();
            services.AddTransient<IJobHistoryService, JobHistoryManager>();
            services.AddTransient<IJobService, JobManager>();
            services.AddTransient<ILocationService, LocationManager>();
            services.AddTransient<IRegionService, RegionManager>();
            services.AddTransient<IServiceManager, ServiceManager>();

            return services;
        }
    }
}
