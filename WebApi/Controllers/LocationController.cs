using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Repositories;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LocationController : ControllerBase
    {
        private readonly ILogger<LocationController> _logger;
        private readonly ILocationRepository _locationRepository;

        public LocationController(ILogger<LocationController> logger, ILocationRepository locationRepository)
        {
            _logger = logger;
            _locationRepository = locationRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Location>> Get()
        {
            var locations = _locationRepository.GetAll();

            return Ok(locations);
        }

        [HttpGet("{id:int}")]
        public ActionResult<Location> Get(int id)
        {
            var location = _locationRepository.Get(id);

            if (location is not null)
                return Ok(location);

            return NotFound();
        }

        [HttpPost()]
        public IActionResult Post([FromBody] Location location)
        {
            var result = _locationRepository.Add(location);

            if (result)
                return Created();

            return BadRequest();
        }

        [HttpPut("{id:int}")]
        public IActionResult Put(int id, [FromBody] Location location)
        {
            var result = _locationRepository.Update(location);

            if (result)
                return NoContent();

            return NotFound();
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var result = _locationRepository.Delete(id);

            if (result)
                return NoContent();

            return NotFound();
        }
    }
}
