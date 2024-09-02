using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Repositories;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly ILogger<EmployeeController> _logger;
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeController(ILogger<EmployeeController> logger, IEmployeeRepository employeeRepository)
        {
            _logger = logger;
            _employeeRepository = employeeRepository;
        }

        [HttpGet(Name = "GetAllEmployee")]
        public ActionResult<IEnumerable<Employee>> Get()
        {
            var employees = _employeeRepository.GetAll();
            return Ok(employees);
        }

        [HttpGet("{id:int}")]
        public ActionResult<Employee> Get(int id)
        {
            var employee = _employeeRepository.Get(id);

            if (employee is null)
                return NotFound();

            return Ok(employee);
        }

        [HttpPost()]
        public IActionResult Post([FromBody] Employee employee)
        {
            var result = _employeeRepository.Add(employee);

            if (result)
                return Created();

            return BadRequest();
        }

        [HttpPut("{id:int}")]
        public IActionResult Put(int id, [FromBody] Employee employee)
        {
            var result = _employeeRepository.Update(employee);

            if (result)
                return NoContent();

            return NotFound();
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var result = _employeeRepository.Delete(id);

            if (result)
                return NoContent();

            return NotFound();
        }
    }
}
