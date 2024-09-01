using Microsoft.AspNetCore.Mvc;
using Oracle.ManagedDataAccess.Client;
using System.Net;
using WebApi.Models;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LocationController : ControllerBase
    {
        private readonly ILogger<LocationController> _logger;

        public LocationController(ILogger<LocationController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Location>> Get()
        {
            string conString = "User Id=HR;Password=HR;Data Source=localhost:1521/FREEPDB1";

            using (OracleConnection con = new OracleConnection(conString))
            {
                using (OracleCommand cmd = con.CreateCommand())
                {
                    con.Open();

                    cmd.CommandText = "select LOCATION_ID, STREET_ADDRESS, POSTAL_CODE, CITY, STATE_PROVINCE, COUNTRY_ID from locations";

                    OracleDataReader reader = cmd.ExecuteReader();

                    List<Location> list = new List<Location>();
                    while (reader.Read())
                    {
                        list.Add(new Location()
                        {
                            LocationId = reader.GetInt32(0),
                            StreetAddress = reader.IsDBNull(1) ? null : reader.GetString(1),
                            PostalCode = reader.IsDBNull(2) ? null : reader.GetString(2),
                            City = reader.GetString(3),
                            StateProvince = reader.IsDBNull(4) ? null : reader.GetString(4),
                            CountryId = reader.IsDBNull(5) ? null : reader.GetString(5)
                        });
                    }

                    return Ok(list);
                }
            }
        }

        [HttpGet("{id:int}")]
        public ActionResult<Location> Get(int id)
        {
            string conString = "User Id=HR;Password=HR;Data Source=localhost:1521/FREEPDB1";

            using (OracleConnection con = new OracleConnection(conString))
            {
                using (OracleCommand cmd = con.CreateCommand())
                {
                    con.Open();

                    cmd.CommandText = "select LOCATION_ID, STREET_ADDRESS, POSTAL_CODE, CITY, STATE_PROVINCE, COUNTRY_ID from locations where location_id = :location_id";

                    OracleParameter idParam = new OracleParameter("location_id", id);
                    cmd.Parameters.Add(idParam);

                    OracleDataReader reader = cmd.ExecuteReader();
                    Location location = null;
                    while (reader.Read())
                    {
                        location = new Location()
                        {
                            LocationId = reader.GetInt32(0),
                            StreetAddress = reader.IsDBNull(1) ? null : reader.GetString(1),
                            PostalCode = reader.IsDBNull(2) ? null : reader.GetString(2),
                            City = reader.GetString(3),
                            StateProvince = reader.IsDBNull(4) ? null : reader.GetString(4),
                            CountryId = reader.IsDBNull(5) ? null : reader.GetString(5)
                        };
                    }

                    if (location != null)
                        return Ok(location);

                    return NotFound();
                }
            }
        }

        [HttpPost()]
        public IActionResult Post([FromBody] Location location)
        {
            string conString = "User Id=HR;Password=HR;Data Source=localhost:1521/FREEPDB1";

            using (OracleConnection con = new OracleConnection(conString))
            {
                using (OracleCommand cmd = con.CreateCommand())
                {
                    con.Open();

                    cmd.CommandText = "insert into locations (LOCATION_ID, STREET_ADDRESS, POSTAL_CODE, CITY, STATE_PROVINCE, COUNTRY_ID) " +
                        "values (:location_id, :street_address, :postal_code, :city, :state_province, :country_id)";

                    OracleParameter idParam = new OracleParameter("location_id", location.LocationId);
                    cmd.Parameters.Add(idParam);
                    OracleParameter streetAddress = new OracleParameter("street_address", location.StreetAddress);
                    cmd.Parameters.Add(streetAddress);
                    OracleParameter postalCode = new OracleParameter("postal_code", location.PostalCode);
                    cmd.Parameters.Add(postalCode);
                    OracleParameter city = new OracleParameter("city", location.City);
                    cmd.Parameters.Add(city);
                    OracleParameter stateProvince = new OracleParameter("state_province", location.StateProvince);
                    cmd.Parameters.Add(stateProvince);
                    OracleParameter countryId = new OracleParameter("country_id", location.CountryId);
                    cmd.Parameters.Add(countryId);

                    int result = cmd.ExecuteNonQuery();

                    if (result > 0)
                        return Created();

                    return BadRequest();
                }
            }
        }

        [HttpPut("{id:int}")]
        public IActionResult Put(int id, [FromBody] Location location)
        {
            string conString = "User Id=HR;Password=HR;Data Source=localhost:1521/FREEPDB1";

            using (OracleConnection con = new OracleConnection(conString))
            {
                using (OracleCommand cmd = con.CreateCommand())
                {
                    con.Open();

                    cmd.CommandText = "update locations c " +
                        "set c.location_id = :location_id," +
                        "c.street_address = :street_address," +
                        "c.postal_code = :postal_code," +
                        "c.city = :city," +
                        "c.state_province = :state_province," +
                        "c.country_id = :country_id " +
                        "where c.location_id = :location_id";

                    OracleParameter idParam = new OracleParameter("location_id", location.LocationId);
                    cmd.Parameters.Add(idParam);
                    OracleParameter streetAddress = new OracleParameter("street_address", location.StreetAddress);
                    cmd.Parameters.Add(streetAddress);
                    OracleParameter postalCode = new OracleParameter("postal_code", location.PostalCode);
                    cmd.Parameters.Add(postalCode);
                    OracleParameter city = new OracleParameter("city", location.City);
                    cmd.Parameters.Add(city);
                    OracleParameter stateProvince = new OracleParameter("state_province", location.StateProvince);
                    cmd.Parameters.Add(stateProvince);
                    OracleParameter countryId = new OracleParameter("country_id", location.CountryId);
                    cmd.Parameters.Add(countryId);


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

                    cmd.CommandText = "Delete from locations c where c.location_id = :location_id";

                    OracleParameter idParam = new OracleParameter("location_id", id);
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
