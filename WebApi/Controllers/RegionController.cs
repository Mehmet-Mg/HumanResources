using Microsoft.AspNetCore.Mvc;
using Oracle.ManagedDataAccess.Client;
using System.Net;
using WebApi.Models;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegionController : ControllerBase
    {
        private readonly ILogger<RegionController> _logger;

        public RegionController(ILogger<RegionController> logger)
        {
            _logger = logger;
        }


        [HttpGet]
        public ActionResult<IEnumerable<Region>> Get()
        {
            string conString = "User Id=HR;Password=HR;Data Source=localhost:1521/FREEPDB1";

            using (OracleConnection con = new OracleConnection(conString))
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

                    return Ok(list);
                }
            }
        }

        [HttpGet("{id:int}")]
        public ActionResult<Region> Get(int id)
        {
            string conString = "User Id=HR;Password=HR;Data Source=localhost:1521/FREEPDB1";

            using (OracleConnection con = new OracleConnection(conString))
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

                    if (region != null)
                        return Ok(region);

                    return NotFound();
                }
            }
        }

        [HttpPost()]
        public IActionResult Post([FromBody] Region region)
        {
            string conString = "User Id=HR;Password=HR;Data Source=localhost:1521/FREEPDB1";

            using (OracleConnection con = new OracleConnection(conString))
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

                    if (result > 0)
                        return Created();

                    return BadRequest();
                }
            }
        }

        [HttpPut("{id:int}")]
        public IActionResult Put(int id, [FromBody] Region region)
        {
            string conString = "User Id=HR;Password=HR;Data Source=localhost:1521/FREEPDB1";

            using (OracleConnection con = new OracleConnection(conString))
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

                    cmd.CommandText = "Delete from regions c where c.region_id = :region_id";

                    OracleParameter idParam = new OracleParameter("region_id", id);
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
