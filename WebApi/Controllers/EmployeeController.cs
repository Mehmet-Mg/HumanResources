using HumanResources.BLL.Services.Contracts;
using HumanResources.DTO.Models;
using Microsoft.AspNetCore.Mvc;

namespace HumanResources.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly ILogger<EmployeeController> _logger;
        private readonly IServiceManager _manager;

        public EmployeeController(ILogger<EmployeeController> logger, IServiceManager manager)
        {
            _logger = logger;
            _manager = manager;
        }

        [HttpGet(Name = "GetAllEmployee")]
        public ActionResult<IEnumerable<Employee>> Get()
        {
            var employees = _manager.EmployeeService.GetAllEmployee();
            return Ok(employees);
        }

        [HttpGet("{id:int}")]
        public ActionResult<Employee> Get(int id)
        {
            var employee = _manager.EmployeeService.GetEmployeeById(id);

            if (employee is null)
                return NotFound();

            return Ok(employee);
        }

        [HttpPost()]
        public IActionResult Post([FromBody] Employee employee)
        {
            var result = _manager.EmployeeService.AddEmployee(employee);

            if (result)
                return Created();

            return BadRequest();
        }

        [HttpPut("{id:int}")]
        public IActionResult Put(int id, [FromBody] Employee employee)
        {
            var result = _manager.EmployeeService.UpdateEmployee(employee);

            if (result)
                return NoContent();

            return NotFound();
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var result = _manager.EmployeeService.DeleteEmployee(id);

            if (result)
                return NoContent();

            return NotFound();
        }
    }
}
