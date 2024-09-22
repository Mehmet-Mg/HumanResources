using HumanResources.DTO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources.BLL.Services.Contracts
{
    public interface IDepartmentService
    {
        IEnumerable<Department> GetAllDepartment();
        Department? GetDepartmentById(int deparmentId);
        bool AddDepartment(Department department);
        bool UpdateDepartment(Department department);
        bool DeleteDepartment(int deparmentId);
    }
}
