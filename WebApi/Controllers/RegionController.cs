using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Repositories;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegionController : ControllerBase
    {
        private readonly ILogger<RegionController> _logger;
        private readonly IRegionRepository _regionRepository;

        public RegionController(ILogger<RegionController> logger, IRegionRepository regionRepository)
        {
            _logger = logger;
            _regionRepository = regionRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Region>> Get()
        {
            var regions = _regionRepository.GetAll();

            return Ok(regions);
        }

        [HttpGet("{id:int}")]
        public ActionResult<Region> Get(int id)
        {
            var region = _regionRepository.Get(id);

            if (region is not null)
                return Ok(region);

            return NotFound();
        }

        [HttpPost()]
        public IActionResult Post([FromBody] Region region)
        {
            var result = _regionRepository.Add(region);

            if (result)
                return Created();

            return BadRequest();
        }

        [HttpPut("{id:int}")]
        public IActionResult Put(int id, [FromBody] Region region)
        {
            var result = _regionRepository.Update(region);

            if (result)
                return NoContent();

            return NotFound();
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var result = _regionRepository.Delete(id);

            if (result)
                return NoContent();

            return NotFound();
        }
    }
}
