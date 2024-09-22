using HumanResources.BLL.Services.Contracts;
using HumanResources.DAL.Repositories;
using HumanResources.DTO.Models;

namespace HumanResources.BLL.Services
{
    public class DepartmentManager : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentManager(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public bool AddDepartment(Department department)
        {
            return _departmentRepository.Add(department);
        }

        public bool DeleteDepartment(int deparmentId)
        {
            return _departmentRepository.Delete(deparmentId);
        }

        public IEnumerable<Department> GetAllDepartment()
        {
            return _departmentRepository.GetAll();
        }

        public Department? GetDepartmentById(int deparmentId)
        {
            return _departmentRepository.Get(deparmentId);
        }

        public bool UpdateDepartment(Department department)
        {
            return _departmentRepository.Update(department);
        }
    }
}
