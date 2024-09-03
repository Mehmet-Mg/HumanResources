using HumanResources.BLL.Services.Contracts;
using HumanResources.DTO.Models;
using Microsoft.AspNetCore.Mvc;

namespace HumanResources.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class JobHistoryController : ControllerBase
    {
        private readonly ILogger<JobHistoryController> _logger;
        private readonly IServiceManager _manager;

        public JobHistoryController(ILogger<JobHistoryController> logger, IServiceManager manager)
        {
            _logger = logger;
            _manager = manager;
        }

        [HttpGet]
        public ActionResult<IEnumerable<JobHistory>> Get()
        {
            var histories = _manager.JobHistoryService.GetAllJobHistory();

            return Ok(histories);
        }

        [HttpGet("{id:int}")]
        public ActionResult<JobHistory> Get(int id, [FromQuery] DateTime startDate)
        {
            var history = _manager.JobHistoryService.GetJobHistoryById(id, startDate);

            if (history is not null)
                return Ok(history);

            return NotFound();
        }

        [HttpPost()]
        public IActionResult Post([FromBody] JobHistory jobHistory)
        {
            var result = _manager.JobHistoryService.AddJobHistory(jobHistory);

            if (result)
                return Created();

            return BadRequest();
        }

        [HttpPut("{id:int}")]
        public IActionResult Put(int id, [FromBody] JobHistory jobHistory)
        {
            var result = _manager.JobHistoryService.UpdateJobHistory(jobHistory);

            if (result)
                return NoContent();

            return NotFound();
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteById(int id)
        {
            var result = _manager.JobHistoryService.DeleteJobHistory(id);

            if (result)
                return NoContent();

            return NotFound();
        }

        [HttpDelete(Name = "DeleteByIdAndStartDate")]
        public IActionResult DeleteByIdAndStartDate([FromQuery] int id, [FromQuery] DateTime startDate)
        {
            var result = _manager.JobHistoryService.DeleteJobHistory(id, startDate);

            if (result)
                return NoContent();

            return NotFound();
        }
    }
}
