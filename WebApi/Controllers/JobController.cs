using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Repositories;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class JobController : ControllerBase
    {
        private readonly ILogger<JobController> _logger;
        private readonly IJobRepository _jobRepository;

        public JobController(ILogger<JobController> logger, IJobRepository jobRepository)
        {
            _logger = logger;
            _jobRepository = jobRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Job>> Get()
        {
            var jobs = _jobRepository.GetAll();

            return Ok(jobs);
        }

        [HttpGet("{id}")]
        public ActionResult<Country> Get(string id)
        {
            var job = _jobRepository.Get(id);

            if (job is not null)
                return Ok(job);

            return NotFound();
        }

        [HttpPost()]
        public IActionResult Post([FromBody] Job job)
        {
            var result = _jobRepository.Add(job);

            if (result)
                return Created();

            return BadRequest();
        }

        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody] Job job)
        {
            var result = _jobRepository.Update(job);

            if (result)
                return NoContent();

            return NotFound();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var result = _jobRepository.Delete(id);

            if (result)
                return NoContent();

            return NotFound();
        }
    }
}
