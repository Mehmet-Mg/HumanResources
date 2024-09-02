
using Oracle.ManagedDataAccess.Client;
using WebApi.Models;

namespace WebApi.Repositories.ManagedDataAccess
{
    public class RegionRepository : IRegionRepository
    {
        private readonly string? _connectionString;

        public RegionRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("OracleDb");
        }

        public bool Add(Region region)
        {
            using (OracleConnection con = new OracleConnection(_connectionString))
            {
                using (OracleCommand cmd = con.CreateCommand())
                {
                    con.Open();

                    cmd.CommandText = "insert into regions (REGION_ID, REGION_NAME) " +
                        "values (:region_id, :region_name)";

                    OracleParameter idParam = new OracleParameter("region_id", region.RegionId);
                    cmd.Parameters.Add(idParam);
                    OracleParameter regionName = new OracleParameter("region_name", region.RegionName);
                    cmd.Parameters.Add(regionName);

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

                    cmd.CommandText = "Delete from regions c where c.region_id = :region_id";

                    OracleParameter idParam = new OracleParameter("region_id", id);
                    cmd.Parameters.Add(idParam);

                    int result = cmd.ExecuteNonQuery();

                    return result > 0;
                }
            }
        }

        public Region Get(int id)
        {
            using (OracleConnection con = new OracleConnection(_connectionString))
            {
                using (OracleCommand cmd = con.CreateCommand())
                {
                    con.Open();

                    cmd.CommandText = "select REGION_ID, REGION_NAME from regions where region_id = :region_id";

                    OracleParameter idParam = new OracleParameter("region_id", id);
                    cmd.Parameters.Add(idParam);

                    OracleDataReader reader = cmd.ExecuteReader();
                    Region region = null;
                    while (reader.Read())
                    {
                        region = new Region()
                        {
                            RegionId = reader.GetInt32(0),
                            RegionName = reader.IsDBNull(1) ? null : reader.GetString(1),
                        };
                    }

                    return region;
                }
            }
        }

        public IEnumerable<Region> GetAll()
        {
            using (OracleConnection con = new OracleConnection(_connectionString))
            {
                using (OracleCommand cmd = con.CreateCommand())
                {
                    con.Open();

                    cmd.CommandText = "select REGION_ID, REGION_NAME from regions";

                    OracleDataReader reader = cmd.ExecuteReader();

                    List<Region> list = new List<Region>();
                    while (reader.Read())
                    {
                        list.Add(new Region()
                        {
                            RegionId = reader.GetInt32(0),
                            RegionName = reader.IsDBNull(1) ? null : reader.GetString(1),
                        });
                    }

                    return list;
                }
            }
        }

        public bool Update(Region region)
        {
            using (OracleConnection con = new OracleConnection(_connectionString))
            {
                using (OracleCommand cmd = con.CreateCommand())
                {
                    con.Open();

                    cmd.CommandText = "update regions c " +
                        "set c.region_id = :region_id," +
                        "c.region_name = :region_name " +
                        "where c.region_id = :region_id";

                    OracleParameter idParam = new OracleParameter("region_id", region.RegionId);
                    cmd.Parameters.Add(idParam);
                    OracleParameter regionName = new OracleParameter("region_name", region.RegionName);
                    cmd.Parameters.Add(regionName);

                    int result = cmd.ExecuteNonQuery();

                    return result > 0;
                }
            }
        }
    }
}
