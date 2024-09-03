using HumanResources.BLL.Services.Contracts;
using HumanResources.DAL.Repositories;
using HumanResources.DTO.Models;

namespace HumanResources.BLL.Services
{
    public class EmployeeManager : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeManager(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public bool AddEmployee(Employee employee)
        {
            return _employeeRepository.Add(employee);
        }

        public bool DeleteEmployee(int employeeId)
        {
            return _employeeRepository.Delete(employeeId);
        }

        public IEnumerable<Employee> GetAllEmployee()
        {
            return _employeeRepository.GetAll();
        }

        public Employee? GetEmployeeById(int employeeId)
        {
            return _employeeRepository.Get(employeeId);
        }

        public bool UpdateEmployee(Employee employee)
        {
            return _employeeRepository.Update(employee);
        }
    }
}
