using Microsoft.AspNetCore.Mvc;
using Oracle.ManagedDataAccess.Client;
using System.Net;
using WebApi.Models;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class JobHistoryController : ControllerBase
    {
        private readonly ILogger<JobHistoryController> _logger;

        public JobHistoryController(ILogger<JobHistoryController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<IEnumerable<JobHistory>> Get()
        {
            string conString = "User Id=HR;Password=HR;Data Source=localhost:1521/FREEPDB1";

            using (OracleConnection con = new OracleConnection(conString))
            {
                using (OracleCommand cmd = con.CreateCommand())
                {
                    con.Open();

                    cmd.CommandText = "select EMPLOYEE_ID, START_DATE, END_DATE, JOB_ID, DEPARTMENT_ID from job_history";

                    OracleDataReader reader = cmd.ExecuteReader();

                    List<JobHistory> list = new List<JobHistory>();
                    while (reader.Read())
                    {
                        list.Add(new JobHistory()
                        {
                            EmployeeId = reader.GetInt32(0),
                            StartDate = reader.GetDateTime(1),
                            EndDate = reader.GetDateTime(2),
                            JobId = reader.GetString(3),
                            DepartmentId = reader.IsDBNull(4) ? null : reader.GetInt32(4)
                        });
                    }

                    return Ok(list);
                }
            }
        }

        [HttpGet("{id:int}")]
        public ActionResult<IEnumerable<JobHistory>> Get(int id, [FromQuery] DateTime? startDate)
        {
            string conString = "User Id=HR;Password=HR;Data Source=localhost:1521/FREEPDB1";

            using (OracleConnection con = new OracleConnection(conString))
            {
                using (OracleCommand cmd = con.CreateCommand())
                {
                    con.Open();

                    cmd.CommandText = "select EMPLOYEE_ID, START_DATE, END_DATE, JOB_ID, DEPARTMENT_ID from job_history where employee_id = :employee_id";

                    OracleParameter idParam = new OracleParameter("employee_id", id);
                    cmd.Parameters.Add(idParam);

                    if (startDate != null)
                    {
                        cmd.CommandText += " and start_date = :start_date";

                        OracleParameter startDateParam = new OracleParameter("start_date", startDate);
                        cmd.Parameters.Add(startDateParam);
                    }

                    OracleDataReader reader = cmd.ExecuteReader();
                    List<JobHistory> list = new List<JobHistory>();
                    while (reader.Read())
                    {
                        list.Add(new JobHistory()
                        {
                            EmployeeId = reader.GetInt32(0),
                            StartDate = reader.GetDateTime(1),
                            EndDate = reader.GetDateTime(2),
                            JobId = reader.GetString(3),
                            DepartmentId = reader.IsDBNull(4) ? null : reader.GetInt32(4)
                        });
                    }

                    return Ok(list);
                }
            }
        }

        [HttpPost()]
        public IActionResult Post([FromBody] JobHistory jobHistory)
        {
            string conString = "User Id=HR;Password=HR;Data Source=localhost:1521/FREEPDB1";

            using (OracleConnection con = new OracleConnection(conString))
            {
                using (OracleCommand cmd = con.CreateCommand())
                {
                    con.Open();

                    cmd.CommandText = "insert into job_history (EMPLOYEE_ID, START_DATE, END_DATE, JOB_ID, DEPARTMENT_ID) " +
                        "values (:employee_id, :start_date, :end_date, :job_id, :department_id)";

                    OracleParameter idParam = new OracleParameter("employee_id", jobHistory.EmployeeId);
                    cmd.Parameters.Add(idParam);
                    OracleParameter startDate = new OracleParameter("start_date", jobHistory.StartDate);
                    cmd.Parameters.Add(startDate);
                    OracleParameter endDate = new OracleParameter("end_date", jobHistory.EndDate);
                    cmd.Parameters.Add(endDate);
                    OracleParameter jobId = new OracleParameter("job_id", jobHistory.JobId);
                    cmd.Parameters.Add(jobId);
                    OracleParameter departmentId = new OracleParameter("department_id", jobHistory.DepartmentId);
                    cmd.Parameters.Add(departmentId);

                    int result = cmd.ExecuteNonQuery();

                    if (result > 0)
                        return Created();

                    return BadRequest();
                }
            }
        }

        [HttpPut("{id:int}")]
        public IActionResult Put(int id, [FromBody] JobHistory jobHistory)
        {
            string conString = "User Id=HR;Password=HR;Data Source=localhost:1521/FREEPDB1";

            using (OracleConnection con = new OracleConnection(conString))
            {
                using (OracleCommand cmd = con.CreateCommand())
                {
                    con.Open();

                    cmd.CommandText = "update job_history j " +
                        "set j.employee_id = :employee_id," +
                        "j.start_date = :start_date," +
                        "j.end_date = :end_date," +
                        "j.job_id = :job_id," +
                        "j.department_id = :department_id " +
                        "where j.employee_id = :employee_id " +
                        "and j.start_date = :start_date";


                    OracleParameter idParam = new OracleParameter("employee_id", jobHistory.EmployeeId);
                    cmd.Parameters.Add(idParam);
                    OracleParameter startDate = new OracleParameter("start_date", jobHistory.StartDate);
                    cmd.Parameters.Add(startDate);
                    OracleParameter endDate = new OracleParameter("end_date", jobHistory.EndDate);
                    cmd.Parameters.Add(endDate);
                    OracleParameter jobId = new OracleParameter("job_id", jobHistory.JobId);
                    cmd.Parameters.Add(jobId);
                    OracleParameter departmentId = new OracleParameter("department_id", jobHistory.DepartmentId);
                    cmd.Parameters.Add(departmentId);

                    int result = cmd.ExecuteNonQuery();

                    if (result > 0)
                        return NoContent();

                    return BadRequest();
                }
            }
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id, [FromQuery] DateTime startDate)
        {
            string conString = "User Id=HR;Password=HR;Data Source=localhost:1521/FREEPDB1";

            using (OracleConnection con = new OracleConnection(conString))
            {
                using (OracleCommand cmd = con.CreateCommand())
                {
                    con.Open();

                    cmd.CommandText = "Delete from job_history j where j.employee_id = :employee_id and j.start_date = :start_date";

                    OracleParameter idParam = new OracleParameter("country_id", id);
                    cmd.Parameters.Add(idParam);
                    OracleParameter startDateParam = new OracleParameter("country_id", startDate);
                    cmd.Parameters.Add(startDateParam);

                    int result = cmd.ExecuteNonQuery();

                    if (result > 0)
                        return NoContent();

                    return BadRequest();
                }
            }
        }
    }
}
