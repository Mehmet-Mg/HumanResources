using Oracle.ManagedDataAccess.Client;
using HumanResources.DTO.Models;
using Microsoft.Extensions.Configuration;

namespace HumanResources.DAL.Repositories.ManagedDataAccess
{
    public class JobHistoryRepository : IJobHistoryRepository
    {
        private readonly string? _connectionString;

        public JobHistoryRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("OracleDb");
        }

        public bool Add(JobHistory jobHistory)
        {
            using (OracleConnection con = new OracleConnection(_connectionString))
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

                    return result > 0;
                }
            }
        }

        public bool Delete(int employeeId, DateTime? startDate)
        {
            using (OracleConnection con = new OracleConnection(_connectionString))
            {
                using (OracleCommand cmd = con.CreateCommand())
                {
                    con.Open();

                    cmd.CommandText = "Delete from job_history j where j.employee_id = :employee_id and j.start_date = :start_date";

                    OracleParameter idParam = new OracleParameter("country_id", employeeId);
                    cmd.Parameters.Add(idParam);
                    OracleParameter startDateParam = new OracleParameter("country_id", startDate);
                    cmd.Parameters.Add(startDateParam);

                    int result = cmd.ExecuteNonQuery();

                    return result > 0;
                }
            }
        }

        public bool Delete(int employeeId)
        {
            using (OracleConnection con = new OracleConnection(_connectionString))
            {
                using (OracleCommand cmd = con.CreateCommand())
                {
                    con.Open();

                    cmd.CommandText = "Delete from job_history j where j.employee_id = :employee_id";

                    OracleParameter idParam = new OracleParameter("country_id", employeeId);
                    cmd.Parameters.Add(idParam);

                    int result = cmd.ExecuteNonQuery();

                    return result > 0;
                }
            }
        }

        public JobHistory Get(int employeeId, DateTime startDate)
        {
            using (OracleConnection con = new OracleConnection(_connectionString))
            {
                using (OracleCommand cmd = con.CreateCommand())
                {
                    con.Open();

                    cmd.CommandText = "select EMPLOYEE_ID, START_DATE, END_DATE, JOB_ID, DEPARTMENT_ID from job_history where employee_id = :employee_id and start_date = :start_date";

                    OracleParameter idParam = new OracleParameter("employee_id", employeeId);
                    cmd.Parameters.Add(idParam);

                    OracleParameter startDateParam = new OracleParameter("start_date", startDate);
                    cmd.Parameters.Add(startDateParam);

                    OracleDataReader reader = cmd.ExecuteReader();
                    JobHistory history = null;
                    while (reader.Read())
                    {
                        history = new JobHistory()
                        {
                            EmployeeId = reader.GetInt32(0),
                            StartDate = reader.GetDateTime(1),
                            EndDate = reader.GetDateTime(2),
                            JobId = reader.GetString(3),
                            DepartmentId = reader.IsDBNull(4) ? null : reader.GetInt32(4)
                        };
                    }

                    return history;
                }
            }
        }

        public JobHistory Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<JobHistory> GetAll()
        {
            using (OracleConnection con = new OracleConnection(_connectionString))
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

                    return list;
                }
            }
        }

        public IEnumerable<JobHistory> GetEmployeeHistory(int employeeId)
        {
            using (OracleConnection con = new OracleConnection(_connectionString))
            {
                using (OracleCommand cmd = con.CreateCommand())
                {
                    con.Open();

                    cmd.CommandText = "select EMPLOYEE_ID, START_DATE, END_DATE, JOB_ID, DEPARTMENT_ID from job_history where employee_id = :employee_id";

                    OracleParameter idParam = new OracleParameter("employee_id", employeeId);
                    cmd.Parameters.Add(idParam);

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

                    return list;
                }
            }
        }

        public bool Update(JobHistory jobHistory)
        {
            using (OracleConnection con = new OracleConnection(_connectionString))
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

                    return result > 0;
                }
            }
        }
    }
}
