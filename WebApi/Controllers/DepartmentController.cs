using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Repositories;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DepartmentController : ControllerBase
    {
        private readonly ILogger<DepartmentController> _logger;
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentController(ILogger<DepartmentController> logger, IDepartmentRepository departmentRepository)
        {
            _logger = logger;
            _departmentRepository = departmentRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Department>> Get()
        {
            var departments = _departmentRepository.GetAll();

            return Ok(departments);
        }

        [HttpGet("{id:int}")]
        public ActionResult<Department> Get(int id)
        {
            var department = _departmentRepository.Get(id);

            if (department is not null)
                return Ok(department);

            return NotFound();
        }

        [HttpPost()]
        public IActionResult Post([FromBody] Department department)
        {
            var result = _departmentRepository.Add(department);

            if (result)
                return Created();

            return BadRequest();
        }

        [HttpPut("{id:int}")]
        public IActionResult Put(int id, [FromBody] Department department)
        {
            var result = _departmentRepository.Update(department);

            if (result)
                return NoContent();

            return BadRequest();
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var result = _departmentRepository.Delete(id);

            if (result)
                return NoContent();

            return BadRequest();
        }
    }
}
