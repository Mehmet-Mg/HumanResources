using Microsoft.AspNetCore.Mvc;
using Oracle.ManagedDataAccess.Client;
using System.Configuration;
using System.Net;
using WebApi.Models;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly ILogger<EmployeeController> _logger;
        private readonly string? _connectionString;

        public EmployeeController(ILogger<EmployeeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _connectionString = configuration.GetConnectionString("OracleDb");
        }

        [HttpGet(Name = "GetAllEmployee")]
        public ActionResult<IEnumerable<Employee>> Get()
        {
            using (OracleConnection con = new OracleConnection(_connectionString))
            {
                using(OracleCommand cmd = con.CreateCommand())
                {
                    con.Open();

                    cmd.CommandText = "select EMPLOYEE_ID, FIRST_NAME, LAST_NAME, EMAIL, PHONE_NUMBER, HIRE_DATE, JOB_ID, SALARY, COMMISSION_PCT, MANAGER_ID, DEPARTMENT_ID from hr.employees";

                    OracleDataReader reader = cmd.ExecuteReader();
                    
                    List<Employee> list = new List<Employee>();
                    while (reader.Read())
                    {
                        list.Add(new Employee()
                        {
                            EmployeeId = reader.GetInt32(0),
                            FirstName = reader.GetString(1),
                            LastName = reader.GetString(2),
                            Email = reader.GetString(3),
                            PhoneNumber = reader.GetString(4),
                            HireDate = reader.GetDateTime(5),
                            JobId = reader.GetString(6),
                            Salary = reader.GetDecimal(7),
                            CommissionPercent = reader.IsDBNull(8) ? default : reader.GetDecimal(8),
                            ManagerId = reader.IsDBNull(9) ? null : reader.GetInt32(9),
                            DepartmentId = reader.IsDBNull(10) ? null : reader.GetInt32(10)
                        });
                    }

                    return Ok(list);
                }
            }
        }

        [HttpGet("{id:int}")]
        public ActionResult<Employee> Get(int id)
        {
            using (OracleConnection con = new OracleConnection(_connectionString))
            {
                using (OracleCommand cmd = con.CreateCommand())
                {
                    con.Open();

                    cmd.CommandText = "select EMPLOYEE_ID, FIRST_NAME, LAST_NAME, EMAIL, PHONE_NUMBER, HIRE_DATE, JOB_ID, SALARY, COMMISSION_PCT, MANAGER_ID, DEPARTMENT_ID from hr.employees where employee_id = :id";

                    OracleParameter idParam = new OracleParameter("id", id);
                    cmd.Parameters.Add(idParam);

                    OracleDataReader reader = cmd.ExecuteReader();
                    Employee employee = null;
                    while (reader.Read())
                    {
                        employee = new Employee()
                        {
                            EmployeeId = reader.GetInt32(0),
                            FirstName = reader.GetString(1),
                            LastName = reader.GetString(2),
                            Email = reader.GetString(3),
                            PhoneNumber = reader.GetString(4),
                            HireDate = reader.GetDateTime(5),
                            JobId = reader.GetString(6),
                            Salary = reader.GetDecimal(7),
                            CommissionPercent = reader.IsDBNull(8) ? default : reader.GetDecimal(8),
                            ManagerId = reader.IsDBNull(9) ? null : reader.GetInt32(9),
                            DepartmentId = reader.IsDBNull(10) ? null : reader.GetInt32(10)
                        };
                    }

                    if (employee != null)
                        return Ok(employee);

                    return NotFound();
                }
            }
        }

        [HttpPost()]
        public IActionResult Post([FromBody] Employee employee)
        {
            using (OracleConnection con = new OracleConnection(_connectionString))
            {
                using (OracleCommand cmd = con.CreateCommand())
                {
                    con.Open();

                    cmd.CommandText = "insert into employees (EMPLOYEE_ID, FIRST_NAME, LAST_NAME, EMAIL, PHONE_NUMBER, HIRE_DATE, JOB_ID, SALARY, COMMISSION_PCT, MANAGER_ID, DEPARTMENT_ID) " +
                        "values (:employee_id, :first_name, :last_name, :email, :phone_number, :hire_date, :job_id, :salary, :commission_pct, :manager_id, :department_id)";

                    OracleParameter idParam = new OracleParameter("employee_id", employee.EmployeeId);
                    cmd.Parameters.Add(idParam);
                    OracleParameter firstName = new OracleParameter("first_name", employee.FirstName);
                    cmd.Parameters.Add(firstName);
                    OracleParameter lastName = new OracleParameter("last_name", employee.LastName);
                    cmd.Parameters.Add(lastName);
                    OracleParameter email = new OracleParameter("email", employee.Email);
                    cmd.Parameters.Add(email);
                    OracleParameter phoneNumber = new OracleParameter("phone_number", employee.PhoneNumber);
                    cmd.Parameters.Add(phoneNumber);
                    OracleParameter hireDate= new OracleParameter("hire_date", employee.HireDate);
                    cmd.Parameters.Add(hireDate);
                    OracleParameter jobId = new OracleParameter("job_id", employee.JobId);
                    cmd.Parameters.Add(jobId);
                    OracleParameter salary = new OracleParameter("salary", employee.Salary);
                    cmd.Parameters.Add(salary);
                    OracleParameter commissionPercent = new OracleParameter("commission_pct", employee.CommissionPercent);
                    cmd.Parameters.Add(commissionPercent);
                    OracleParameter managerId = new OracleParameter("manager_id", employee.ManagerId);
                    cmd.Parameters.Add(managerId);
                    OracleParameter departmentId = new OracleParameter("department_id", employee.DepartmentId);
                    cmd.Parameters.Add(departmentId);


                    int result = cmd.ExecuteNonQuery();

                    if (result > 0)
                        return Created();

                    return BadRequest();
                }
            }
        }

        [HttpPut("{id:int}")]
        public IActionResult Put(int id, [FromBody] Employee employee)
        {
            using (OracleConnection con = new OracleConnection(_connectionString))
            {
                using (OracleCommand cmd = con.CreateCommand())
                {
                    con.Open();

                    cmd.CommandText = "update employees e " +
                        "set e.first_name = :first_name," +
                        "e.last_name = :last_name," +
                        "e.email = :email," +
                        "e.phone_number = :phone_number," +
                        "e.Hire_Date = :hire_date," +
                        "e.JOB_ID = :job_id," +
                        "e.Salary = :salary," +
                        "e.Commission_Pct = :commission_pct," +
                        "e.manager_id = :manager_id," +
                        "e.department_id = :department_id " +
                        "where e.employee_id = :employee_id";

                    OracleParameter firstName = new OracleParameter("first_name", employee.FirstName);
                    cmd.Parameters.Add(firstName);
                    OracleParameter lastName = new OracleParameter("last_name", employee.LastName);
                    cmd.Parameters.Add(lastName);
                    OracleParameter email = new OracleParameter("email", employee.Email);
                    cmd.Parameters.Add(email);
                    OracleParameter phoneNumber = new OracleParameter("phone_number", employee.PhoneNumber);
                    cmd.Parameters.Add(phoneNumber);
                    OracleParameter hireDate = new OracleParameter("hire_date", employee.HireDate);
                    cmd.Parameters.Add(hireDate);
                    OracleParameter jobId = new OracleParameter("job_id", employee.JobId);
                    cmd.Parameters.Add(jobId);
                    OracleParameter salary = new OracleParameter("salary", employee.Salary);
                    cmd.Parameters.Add(salary);
                    OracleParameter commissionPercent = new OracleParameter("commission_pct", employee.CommissionPercent);
                    cmd.Parameters.Add(commissionPercent);
                    OracleParameter managerId = new OracleParameter("manager_id", employee.ManagerId);
                    cmd.Parameters.Add(managerId);
                    OracleParameter departmentId = new OracleParameter("department_id", employee.DepartmentId);
                    cmd.Parameters.Add(departmentId);
                    OracleParameter idParam = new OracleParameter("employee_id", id);
                    cmd.Parameters.Add(idParam);


                    int result = cmd.ExecuteNonQuery();

                    if (result > 0)
                        return NoContent();

                    return BadRequest();
                }
            }
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            using (OracleConnection con = new OracleConnection(_connectionString))
            {
                using (OracleCommand cmd = con.CreateCommand())
                {
                    con.Open();

                    cmd.CommandText = "Delete from job_history e where e.employee_id = :employee_id";

                    OracleParameter idParam = new OracleParameter("employee_id", id);
                    cmd.Parameters.Add(idParam);

                    int result = cmd.ExecuteNonQuery();

                    cmd.CommandText = "Delete from employees e where e.employee_id = :employee_id";

                    result = cmd.ExecuteNonQuery();

                    if (result > 0)
                        return NoContent();

                    return BadRequest();
                }
            }
        }
    }
}
