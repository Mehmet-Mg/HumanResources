using Oracle.ManagedDataAccess.Client;
using HumanResources.DTO.Models;
using Microsoft.Extensions.Configuration;

namespace HumanResources.DAL.Repositories.ManagedDataAccess
{
    public class CountryRepository : ICountryRepository
    {
        private readonly string? _connectionString;

        public CountryRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("OracleDb");
        }

        public bool Add(Country country)
        {
            using (OracleConnection con = new OracleConnection(_connectionString))
            {
                using (OracleCommand cmd = con.CreateCommand())
                {
                    con.Open();

                    cmd.CommandText = "insert into countries (COUNTRY_ID, COUNTRY_NAME, REGION_ID) " +
                    "values (:country_id, :country_name, :region_id)";

                    OracleParameter idParam = new OracleParameter("country_id", country.CountryId);
                    cmd.Parameters.Add(idParam);
                    OracleParameter countryName = new OracleParameter("country_name", country.CountryName);
                    cmd.Parameters.Add(countryName);
                    OracleParameter regionId = new OracleParameter("region_id", country.RegionId);
                    cmd.Parameters.Add(regionId);

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

                    cmd.CommandText = "Delete from countries c where c.country_id = :country_id";

                    OracleParameter idParam = new OracleParameter("country_id", id);
                    cmd.Parameters.Add(idParam);

                    int result = cmd.ExecuteNonQuery();

                    return result > 0;
                }
            }
        }

        public Country Get(string id)
        {
            using (OracleConnection con = new OracleConnection(_connectionString))
            {
                using (OracleCommand cmd = con.CreateCommand())
                {
                    con.Open();

                    cmd.CommandText = "select COUNTRY_ID, COUNTRY_NAME, REGION_ID from countries where COUNTRY_ID = :country_id";

                    OracleParameter idParam = new OracleParameter("country_id", id);
                    cmd.Parameters.Add(idParam);

                    OracleDataReader reader = cmd.ExecuteReader();
                    Country country = null;
                    while (reader.Read())
                    {
                        country = new Country()
                        {
                            CountryId = reader.GetString(0),
                            CountryName = reader.GetString(1),
                            RegionId = reader.GetInt32(2),
                        };
                    }

                    return country;
                }
            }
        }

        public IEnumerable<Country> GetAll()
        {
            using (OracleConnection con = new OracleConnection(_connectionString))
            {
                using (OracleCommand cmd = con.CreateCommand())
                {
                    con.Open();

                    cmd.CommandText = "select COUNTRY_ID, COUNTRY_NAME, REGION_ID from countries";

                    OracleDataReader reader = cmd.ExecuteReader();

                    List<Country> list = new List<Country>();
                    while (reader.Read())
                    {
                        list.Add(new Country()
                        {
                            CountryId = reader.GetString(0),
                            CountryName = reader.GetString(1),
                            RegionId = reader.GetInt32(2),
                        });
                    }

                    return list;
                }
            }
        }

        public bool Update(Country country)
        {
            using (OracleConnection con = new OracleConnection(_connectionString))
            {
                using (OracleCommand cmd = con.CreateCommand())
                {
                    con.Open();

                    cmd.CommandText = "update countries c " +
                        "set c.country_id = :country_id," +
                        "c.country_name = :country_name," +
                        "c.region_id = :region_id " +
                        "where c.country_id = :country_id";

                    OracleParameter idParam = new OracleParameter("country_id", country.CountryId);
                    cmd.Parameters.Add(idParam);
                    OracleParameter countryName = new OracleParameter("country_name", country.CountryName);
                    cmd.Parameters.Add(countryName);
                    OracleParameter regionId = new OracleParameter("region_id", country.RegionId);
                    cmd.Parameters.Add(regionId);

                    int result = cmd.ExecuteNonQuery();

                    return result > 0;
                }
            }
        }
    }
}
