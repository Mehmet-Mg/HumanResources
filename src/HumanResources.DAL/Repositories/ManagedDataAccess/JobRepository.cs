using Oracle.ManagedDataAccess.Client;
using HumanResources.DTO.Models;
using Microsoft.Extensions.Configuration;

namespace HumanResources.DAL.Repositories.ManagedDataAccess
{
    public class JobRepository : IJobRepository
    {
        private readonly string? _connectionString;

        public JobRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("OracleDb");
        }

        public bool Add(Job job)
        {
            using (OracleConnection con = new OracleConnection(_connectionString))
            {
                using (OracleCommand cmd = con.CreateCommand())
                {
                    con.Open();

                    cmd.CommandText = "insert into jobs (JOB_ID, JOB_TITLE, MIN_SALARY, MAX_SALARY) " +
                        "values (:job_id, :job_title, :min_salary, :max_salary)";

                    OracleParameter idParam = new OracleParameter("job_id", job.JobId);
                    cmd.Parameters.Add(idParam);
                    OracleParameter jobTitle = new OracleParameter("job_title", job.JobTitle);
                    cmd.Parameters.Add(jobTitle);
                    OracleParameter minSalary = new OracleParameter("min_salary", job.MinSalary);
                    cmd.Parameters.Add(minSalary);
                    OracleParameter maxSalary = new OracleParameter("max_salary", job.MaxSalary);
                    cmd.Parameters.Add(maxSalary);

                    int result = cmd.ExecuteNonQuery();

                    return result > 0;
                }
            }
        }

        public bool Delete(string id)
        {
            using (OracleConnection con = new OracleConnection(_connectionString))
            {
                using (OracleCommand cmd = con.CreateCommand())
                {
                    con.Open();

                    cmd.CommandText = "Delete from jobs j where j.job_id = :job_id";

                    OracleParameter idParam = new OracleParameter("job_id", id);
                    cmd.Parameters.Add(idParam);

                    int result = cmd.ExecuteNonQuery();

                    return result > 0;
                }
            }
        }

        public Job Get(string id)
        {
            using (OracleConnection con = new OracleConnection(_connectionString))
            {
                using (OracleCommand cmd = con.CreateCommand())
                {
                    con.Open();

                    cmd.CommandText = "select JOB_ID, JOB_TITLE, MIN_SALARY, MAX_SALARY from jobs where JOB_ID = :job_id";

                    OracleParameter idParam = new OracleParameter("job_id", id);
                    cmd.Parameters.Add(idParam);

                    OracleDataReader reader = cmd.ExecuteReader();
                    Job job = null;
                    while (reader.Read())
                    {
                        job = new Job()
                        {
                            JobId = reader.GetString(0),
                            JobTitle = reader.GetString(1),
                            MinSalary = reader.IsDBNull(2) ? null : reader.GetDecimal(2),
                            MaxSalary = reader.IsDBNull(3) ? null : reader.GetDecimal(3),
                        };
                    }

                    return job;
                }
            }
        }

        public IEnumerable<Job> GetAll()
        {
            using (OracleConnection con = new OracleConnection(_connectionString))
            {
                using (OracleCommand cmd = con.CreateCommand())
                {
                    con.Open();

                    cmd.CommandText = "select JOB_ID, JOB_TITLE, MIN_SALARY, MAX_SALARY from jobs";

                    OracleDataReader reader = cmd.ExecuteReader();

                    List<Job> list = new List<Job>();
                    while (reader.Read())
                    {
                        list.Add(new Job()
                        {
                            JobId = reader.GetString(0),
                            JobTitle = reader.GetString(1),
                            MinSalary = reader.IsDBNull(2) ? null : reader.GetDecimal(2),
                            MaxSalary = reader.IsDBNull(3) ? null : reader.GetDecimal(3),
                        });
                    }

                    return list;
                }
            }
        }

        public bool Update(Job job)
        {
            using (OracleConnection con = new OracleConnection(_connectionString))
            {
                using (OracleCommand cmd = con.CreateCommand())
                {
                    con.Open();

                    cmd.CommandText = "update jobs j " +
                        "set j.job_id = :job_id," +
                        "j.job_title = :job_title," +
                        "j.min_salary = :min_salary," +
                        "j.max_salary = :max_salary " +
                        "where j.job_id = :job_id";

                    OracleParameter idParam = new OracleParameter("job_id", job.JobId);
                    cmd.Parameters.Add(idParam);
                    OracleParameter jobTitle = new OracleParameter("job_title", job.JobTitle);
                    cmd.Parameters.Add(jobTitle);
                    OracleParameter minSalary = new OracleParameter("min_salary", job.MinSalary);
                    cmd.Parameters.Add(minSalary);
                    OracleParameter maxSalary = new OracleParameter("max_salary", job.MaxSalary);
                    cmd.Parameters.Add(maxSalary);

                    int result = cmd.ExecuteNonQuery();

                    return result > 0;
                }
            }
        }
    }
}
