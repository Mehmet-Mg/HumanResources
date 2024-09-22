using HumanResources.DAL.Repositories;
using HumanResources.DAL.Repositories.ManagedDataAccess;
using Microsoft.Extensions.DependencyInjection;

namespace HumanResources.DAL
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDataAccessLayer(this IServiceCollection services)
        {
            services.AddTransient<ICountryRepository, CountryRepository>();
            services.AddTransient<IDepartmentRepository, DepartmentRepository>();
            services.AddTransient<IEmployeeRepository, EmployeeRepository>();
            services.AddTransient<IJobHistoryRepository, JobHistoryRepository>();
            services.AddTransient<IJobRepository, JobRepository>();
            services.AddTransient<ILocationRepository, LocationRepository>();
            services.AddTransient<IRegionRepository, RegionRepository>();

            return services;
        }
    }
}
