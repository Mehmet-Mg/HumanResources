using Microsoft.AspNetCore.Mvc;
using Oracle.ManagedDataAccess.Client;
using System.Net;
using WebApi.Models;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CountryController : ControllerBase
    {
        private readonly ILogger<CountryController> _logger;
        private readonly string? _connectionString;

        public CountryController(ILogger<CountryController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _connectionString = configuration.GetConnectionString("OracleDb");
        }

        [HttpGet(Name = "GetAllCountry")]
        public ActionResult<IEnumerable<Country>> Get()
        {
            using (OracleConnection con = new OracleConnection(_connectionString))
            {
                using(OracleCommand cmd = con.CreateCommand())
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

                    return Ok(list);
                }
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Country> Get(string id)
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

                    if (country != null)
                        return Ok(country);

                    return NotFound();
                }
            }
        }

        [HttpPost()]
        public IActionResult Post([FromBody] Country country)
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

                    if (result > 0)
                        return Created();

                    return BadRequest();
                }
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody] Country country)
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

                    if (result > 0)
                        return NoContent();

                    return BadRequest();
                }
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
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

                    if (result > 0)
                        return NoContent();

                    return BadRequest();
                }
            }
        }
    }
}
