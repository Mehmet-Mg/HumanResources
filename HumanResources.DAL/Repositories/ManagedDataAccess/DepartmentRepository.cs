using Oracle.ManagedDataAccess.Client;
using HumanResources.DTO.Models;
using Microsoft.Extensions.Configuration;

namespace HumanResources.DAL.Repositories.ManagedDataAccess
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly string? _connectionString;

        public DepartmentRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("OracleDb");
        }

        public bool Add(Department department)
        {
            using (OracleConnection con = new OracleConnection(_connectionString))
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

                    return result > 0;
                }
            }
        }

        public bool Delete(int id)
        {
            using (OracleConnection con = new OracleConnection(_connectionString))
            {
                using (OracleCommand cmd = con.CreateCommand())
                {
                    con.Open();

                    cmd.CommandText = "Delete from departments d where d.department_id = :department_id";

                    OracleParameter idParam = new OracleParameter("department_id", id);
                    cmd.Parameters.Add(idParam);

                    int result = cmd.ExecuteNonQuery();

                    return result > 0;
                }
            }
        }

        public Department Get(int id)
        {
            using (OracleConnection con = new OracleConnection(_connectionString))
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

                    return department;
                }
            }
        }

        public IEnumerable<Department> GetAll()
        {
            using (OracleConnection con = new OracleConnection(_connectionString))
            {
                using (OracleCommand cmd = con.CreateCommand())
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
                            LocationId = reader.IsDBNull(3) ? null : reader.GetInt32(3),
                        });
                    }

                    return list;
                }
            }
        }

        public bool Update(Department department)
        {
            using (OracleConnection con = new OracleConnection(_connectionString))
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

                    return result > 0;
                }
            }
        }
    }
}
