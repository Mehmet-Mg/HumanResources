using HumanResources.BLL.Services.Contracts;
using HumanResources.DTO.Models;
using Microsoft.AspNetCore.Mvc;

namespace HumanResources.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegionController : ControllerBase
    {
        private readonly ILogger<RegionController> _logger;
        private readonly IServiceManager _manager;

        public RegionController(ILogger<RegionController> logger, IServiceManager manager)
        {
            _logger = logger;
            _manager = manager;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Region>> Get()
        {
            var regions = _manager.RegionService.GetAllRegion();

            return Ok(regions);
        }

        [HttpGet("{id:int}")]
        public ActionResult<Region> Get(int id)
        {
            var region = _manager.RegionService.GetRegionById(id);

            if (region is not null)
                return Ok(region);

            return NotFound();
        }

        [HttpPost()]
        public IActionResult Post([FromBody] Region region)
        {
            var result = _manager.RegionService.AddRegion(region);

            if (result)
                return Created();

            return BadRequest();
        }

        [HttpPut("{id:int}")]
        public IActionResult Put(int id, [FromBody] Region region)
        {
            var result = _manager.RegionService.UpdateRegion(region);

            if (result)
                return NoContent();

            return NotFound();
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var result = _manager.RegionService.DeleteRegion(id);

            if (result)
                return NoContent();

            return NotFound();
        }
    }
}
