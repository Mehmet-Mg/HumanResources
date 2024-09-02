using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Repositories;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class JobHistoryController : ControllerBase
    {
        private readonly ILogger<JobHistoryController> _logger;
        private readonly IJobHistoryRepository _jobHistoryRepository;

        public JobHistoryController(ILogger<JobHistoryController> logger, IJobHistoryRepository jobHistoryRepository)
        {
            _logger = logger;
            _jobHistoryRepository = jobHistoryRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<JobHistory>> Get()
        {
            var histories = _jobHistoryRepository.GetAll();

            return Ok(histories);
        }

        [HttpGet("{id:int}")]
        public ActionResult<JobHistory> Get(int id, [FromQuery] DateTime startDate)
        {
            var history = _jobHistoryRepository.Get(id, startDate);

            if (history is not null)
                return Ok(history);

            return NotFound();
        }

        [HttpPost()]
        public IActionResult Post([FromBody] JobHistory jobHistory)
        {
            var result = _jobHistoryRepository.Add(jobHistory);

            if (result)
                return Created();

            return BadRequest();
        }

        [HttpPut("{id:int}")]
        public IActionResult Put(int id, [FromBody] JobHistory jobHistory)
        {
            var result = _jobHistoryRepository.Update(jobHistory);

            if (result)
                return NoContent();

            return NotFound();
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteById(int id)
        {
            var result = _jobHistoryRepository.Delete(id);

            if (result)
                return NoContent();

            return NotFound();
        }

        [HttpDelete(Name = "DeleteByIdAndStartDate")]
        public IActionResult DeleteByIdAndStartDate([FromQuery] int id, [FromQuery] DateTime startDate)
        {
            var result = _jobHistoryRepository.Delete(id, startDate);

            if (result)
                return NoContent();

            return NotFound();
        }
    }
}
