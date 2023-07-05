using AmitWebApis.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AmitWebApis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentRepository departmentRepository;

        public DepartmentController(IDepartmentRepository departmentRepository)
        {
            this.departmentRepository = departmentRepository;
        }

        // GET: api/<DepartmentController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok(await departmentRepository.GetDepartments());
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error Retreving data from database");

            }
        }

        // GET api/<DepartmentController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDepartment(int id)
        {
            try
            {
                var result = await departmentRepository.GetDepartmentById(id);
                if(result==null)
                {
                    return NotFound($"Department does not exists");
                }
                return Ok(result);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error Retreving data from database");
            }
        }

        // POST api/<DepartmentController>
        [HttpPost]
        public async Task<ActionResult<Department>> Post(Department department)
        {
            try
            {
                if(department==null)
                {
                    return BadRequest();
                }
                var dept = await departmentRepository.AddDepartment(department);
                return CreatedAtAction(nameof(GetDepartment), new {id=dept.DepartmentId},dept);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error Adding Department in database");
            }
        }

        // PUT api/<DepartmentController>/5
        [HttpPut("{id:int}")]
        public async Task<ActionResult<Department>> Put(int id, Department department)
        {
            try
            {
                if(id!=department.DepartmentId)
                {
                    return BadRequest("Department Id Mismatch!");
                }
                var dept = await departmentRepository.GetDepartmentById(id);
                if(dept==null)
                {
                    return NotFound($"Department with Id={id} not found");
                }
                return await departmentRepository.UpdateDepartment(department);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error Updating Department in database");
            }
        }

        // DELETE api/<DepartmentController>/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var dept=await departmentRepository.GetDepartmentById(id);
                if(dept==null)
                {
                    return NotFound($"Department with Id={id} not found");
                }
                await departmentRepository.DeleteDepartment(id);
                return Ok($"Department with id {id} has been deleted successfully");
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error Deleting Department in database");
            }
        }
    }
}
