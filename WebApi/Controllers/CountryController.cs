using HumanResources.BLL.Services.Contracts;
using HumanResources.DTO.Models;
using Microsoft.AspNetCore.Mvc;

namespace HumanResources.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CountryController : ControllerBase
    {
        private readonly ILogger<CountryController> _logger;
        private readonly IServiceManager _manager;

        public CountryController(ILogger<CountryController> logger, IServiceManager manager)
        {
            _logger = logger;
            _manager = manager;
        }

        [HttpGet(Name = "GetAllCountry")]
        public ActionResult<IEnumerable<Country>> Get()
        {
            var countries = _manager.CountryService.GetAllCountry();
            return Ok(countries);
        }

        [HttpGet("{id}")]
        public ActionResult<Country> Get(string id)
        {
            var country = _manager.CountryService.GetCountryById(id);

            if (country is not null)
                return Ok(country);

            return NotFound();
        }

        [HttpPost()]
        public IActionResult Post([FromBody] Country country)
        {
            var result = _manager.CountryService.AddCountry(country);

            if (result)
                return Created();

            return BadRequest();
        }

        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody] Country country)
        {
            var result = _manager.CountryService.UpdateCountry(country);

            if (result)
                return NoContent();

            return NotFound();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var result = _manager.CountryService.DeleteCountry(id);

            if (result)
                return NoContent();

            return NotFound();
        }
    }
}
