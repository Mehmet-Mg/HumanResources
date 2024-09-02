using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Repositories;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CountryController : ControllerBase
    {
        private readonly ILogger<CountryController> _logger;
        private readonly ICountryRepository _countryRepository;

        public CountryController(ILogger<CountryController> logger, ICountryRepository countryRepository)
        {
            _logger = logger;
            _countryRepository = countryRepository;
        }

        [HttpGet(Name = "GetAllCountry")]
        public ActionResult<IEnumerable<Country>> Get()
        {
            var countries = _countryRepository.GetAll();
            return Ok(countries);
        }

        [HttpGet("{id}")]
        public ActionResult<Country> Get(string id)
        {
            var country = _countryRepository.Get(id);

            if (country is not null)
                return Ok(country);

            return NotFound();
        }

        [HttpPost()]
        public IActionResult Post([FromBody] Country country)
        {
            var result = _countryRepository.Add(country);

            if (result)
                return Created();

            return BadRequest();
        }

        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody] Country country)
        {
            var result = _countryRepository.Update(country);

            if (result)
                return NoContent();

            return NotFound();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var result = _countryRepository.Delete(id);

            if (result)
                return NoContent();

            return NotFound();
        }
    }
}
