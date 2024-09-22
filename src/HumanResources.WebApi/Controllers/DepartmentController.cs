using HumanResources.BLL.Services.Contracts;
using HumanResources.DTO.Models;
using Microsoft.AspNetCore.Mvc;

namespace HumanResources.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DepartmentController : ControllerBase
    {
        private readonly ILogger<DepartmentController> _logger;
        private readonly IServiceManager _manager;

        public DepartmentController(ILogger<DepartmentController> logger, IServiceManager manager)
        {
            _logger = logger;
            _manager = manager;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Department>> Get()
        {
            var departments = _manager.DepartmentService.GetAllDepartment();

            return Ok(departments);
        }

        [HttpGet("{id:int}")]
        public ActionResult<Department> Get(int id)
        {
            var department = _manager.DepartmentService.GetDepartmentById(id);

            if (department is not null)
                return Ok(department);

            return NotFound();
        }

        [HttpPost()]
        public IActionResult Post([FromBody] Department department)
        {
            var result = _manager.DepartmentService.AddDepartment(department);

            if (result)
                return Created();

            return BadRequest();
        }

        [HttpPut("{id:int}")]
        public IActionResult Put(int id, [FromBody] Department department)
        {
            var result = _manager.DepartmentService.UpdateDepartment(department);

            if (result)
                return NoContent();

            return BadRequest();
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var result = _manager.DepartmentService.DeleteDepartment(id);

            if (result)
                return NoContent();

            return BadRequest();
        }
    }
}
