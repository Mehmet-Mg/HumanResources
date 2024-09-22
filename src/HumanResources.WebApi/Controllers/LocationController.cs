using HumanResources.BLL.Services.Contracts;
using HumanResources.DTO.Models;
using Microsoft.AspNetCore.Mvc;

namespace HumanResources.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LocationController : ControllerBase
    {
        private readonly ILogger<LocationController> _logger;
        private readonly IServiceManager _manager;

        public LocationController(ILogger<LocationController> logger, IServiceManager manager)
        {
            _logger = logger;
            _manager = manager;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Location>> Get()
        {
            var locations = _manager.LocationService.GetAllLocation();

            return Ok(locations);
        }

        [HttpGet("{id:int}")]
        public ActionResult<Location> Get(int id)
        {
            var location = _manager.LocationService.GetLocationById(id);

            if (location is not null)
                return Ok(location);

            return NotFound();
        }

        [HttpPost()]
        public IActionResult Post([FromBody] Location location)
        {
            var result = _manager.LocationService.AddLocation(location);

            if (result)
                return Created();

            return BadRequest();
        }

        [HttpPut("{id:int}")]
        public IActionResult Put(int id, [FromBody] Location location)
        {
            var result = _manager.LocationService.UpdateLocation(location);

            if (result)
                return NoContent();

            return NotFound();
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var result = _manager.LocationService.DeleteLocation(id);

            if (result)
                return NoContent();

            return NotFound();
        }
    }
}
