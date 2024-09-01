using Microsoft.AspNetCore.Mvc;
using Oracle.ManagedDataAccess.Client;
using System.Net;
using WebApi.Models;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DepartmentController : ControllerBase
    {
        private readonly ILogger<DepartmentController> _logger;

        public DepartmentController(ILogger<DepartmentController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Department>> Get()
        {
            string conString = "User Id=HR;Password=HR;Data Source=localhost:1521/FREEPDB1";

            using (OracleConnection con = new OracleConnection(conString))
            {
                using(OracleCommand cmd = con.CreateCommand())
                {
                    con.Open();

                    cmd.CommandText = "select DEPARTMENT_ID, DEPARTMENT_NAME, MANAGER_ID, LOCATION_ID from departments";

                    OracleDataReader reader = cmd.ExecuteReader();
                    
                    List<Department> list = new List<Department>();
                    while (reader.Read())
                    {
                        list.Add(new Department()
                        {
                            DepartmentId = reader.GetInt32(0),
                            DepartmentName = reader.GetString(1),
                            ManagerId = reader.IsDBNull(2) ? null : reader.GetInt32(2),
                            LocationId = reader.IsDBNull(3) ? null :  reader.GetInt32(3),
                        });
                    }

                    return Ok(list);
                }
            }
        }

        [HttpGet("{id:int}")]
        public ActionResult<Department> Get(int id)
        {
            string conString = "User Id=HR;Password=HR;Data Source=localhost:1521/FREEPDB1";

            using (OracleConnection con = new OracleConnection(conString))
            {
                using (OracleCommand cmd = con.CreateCommand())
                {
                    con.Open();

                    cmd.CommandText = "select DEPARTMENT_ID, DEPARTMENT_NAME, MANAGER_ID, LOCATION_ID from departments where department_id = :department_id";

                    OracleParameter idParam = new OracleParameter("department_id", id);
                    cmd.Parameters.Add(idParam);

                    OracleDataReader reader = cmd.ExecuteReader();
                    Department department = null;
                    while (reader.Read())
                    {
                        department = new Department()
                        {
                            DepartmentId = reader.GetInt32(0),
                            DepartmentName = reader.GetString(1),
                            ManagerId = reader.IsDBNull(2) ? null : reader.GetInt32(2),
                            LocationId = reader.IsDBNull(3) ? null : reader.GetInt32(3),
                        };
                    }

                    if (department != null)
                        return Ok(department);

                    return NotFound();
                }
            }
        }

        [HttpPost()]
        public IActionResult Post([FromBody] Department department)
        {
            string conString = "User Id=HR;Password=HR;Data Source=localhost:1521/FREEPDB1";

            using (OracleConnection con = new OracleConnection(conString))
            {
                using (OracleCommand cmd = con.CreateCommand())
                {
                    con.Open();

                    cmd.CommandText = "insert into departments ( DEPARTMENT_ID, DEPARTMENT_NAME, MANAGER_ID, LOCATION_ID) " +
                        "values (:department_id, :department_name, :manager_id, :location_id)";

                    OracleParameter idParam = new OracleParameter("department_id", department.DepartmentId);
                    cmd.Parameters.Add(idParam);
                    OracleParameter departmentName = new OracleParameter("department_name", department.DepartmentName);
                    cmd.Parameters.Add(departmentName);
                    OracleParameter managerId = new OracleParameter("manager_id", department.ManagerId);
                    cmd.Parameters.Add(managerId);
                    OracleParameter locationId = new OracleParameter("location_id", department.LocationId);
                    cmd.Parameters.Add(locationId);

                    int result = cmd.ExecuteNonQuery();

                    if (result > 0)
                        return Created();

                    return BadRequest();
                }
            }
        }

        [HttpPut("{id:int}")]
        public IActionResult Put(int id, [FromBody] Department department)
        {
            string conString = "User Id=HR;Password=HR;Data Source=localhost:1521/FREEPDB1";

            using (OracleConnection con = new OracleConnection(conString))
            {
                using (OracleCommand cmd = con.CreateCommand())
                {
                    con.Open();

                    cmd.CommandText = "update departments d " +
                        "set d.department_name = :department_name," +
                        "d.manager_id = :manager_id," +
                        "d.location_id = :location_id " +
                        "where d.department_id = :department_id";


                    OracleParameter departmentName = new OracleParameter("department_name", department.DepartmentName);
                    cmd.Parameters.Add(departmentName);
                    OracleParameter managerId = new OracleParameter("manager_id", department.ManagerId);
                    cmd.Parameters.Add(managerId);
                    OracleParameter locationId = new OracleParameter("location_id", department.LocationId);
                    cmd.Parameters.Add(locationId);
                    OracleParameter idParam = new OracleParameter("department_id", department.DepartmentId);
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
            string conString = "User Id=HR;Password=HR;Data Source=localhost:1521/FREEPDB1";

            using (OracleConnection con = new OracleConnection(conString))
            {
                using (OracleCommand cmd = con.CreateCommand())
                {
                    con.Open();

                    cmd.CommandText = "Delete from departments d where d.department_id = :department_id";

                    OracleParameter idParam = new OracleParameter("department_id", id);
                    cmd.Parameters.Add(idParam);

                    int result = cmd.ExecuteNonQuery();

                    if (result > 0)
                        return NoContent();

                    return BadRequest();
                }
            }
        }
    }
}
