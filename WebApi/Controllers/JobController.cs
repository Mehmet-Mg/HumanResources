using HumanResources.BLL.Services.Contracts;
using HumanResources.DTO.Models;
using Microsoft.AspNetCore.Mvc;

namespace HumanResources.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class JobController : ControllerBase
    {
        private readonly ILogger<JobController> _logger;
        private readonly IServiceManager _manager;

        public JobController(ILogger<JobController> logger, IServiceManager manager)
        {
            _logger = logger;
            _manager = manager;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Job>> Get()
        {
            var jobs = _manager.JobService.GetAllJob();

            return Ok(jobs);
        }

        [HttpGet("{id}")]
        public ActionResult<Country> Get(string id)
        {
            var job = _manager.JobService.GetJobById(id);

            if (job is not null)
                return Ok(job);

            return NotFound();
        }

        [HttpPost()]
        public IActionResult Post([FromBody] Job job)
        {
            var result = _manager.JobService.AddJob(job);

            if (result)
                return Created();

            return BadRequest();
        }

        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody] Job job)
        {
            var result = _manager.JobService.UpdateJob(job);

            if (result)
                return NoContent();

            return NotFound();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var result = _manager.JobService.DeleteJob(id);

            if (result)
                return NoContent();

            return NotFound();
        }
    }
}
