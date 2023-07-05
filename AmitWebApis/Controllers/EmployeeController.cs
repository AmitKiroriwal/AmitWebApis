using AmitWebApis.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AmitWebApis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository employeeRepository;

        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }

        // GET: api/<EmployeeController>
        [HttpGet("all")]
        public async Task <IActionResult> Get()
        {
            try
            {
                return Ok(await employeeRepository.GetEmployees());
            }
            catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error Retreving data from Database");
            }
        }

        // GET api/<EmployeeController>/5
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetEmployee(int id)
        {
            try
            {
                var result=await employeeRepository.GetEmployeeById(id);
                if(result==null)
                {
                    return NotFound(new { Response = "Employee does not Exist" });
                }
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error Retreving data from Database");
            }
        }

        [HttpGet("{search}")]
           public async Task<ActionResult<IEnumerable<Employee>>> Search(string name, Gender? sex)
           {
            try
            {
                var result = await employeeRepository.SearchEmployee(name, sex);
                if (result.Any())
                    return Ok(result);
                return NotFound();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error Retreving data from database");
            }
        }
        // POST api/<EmployeeController>
        [HttpPost]
        public async Task<ActionResult<Employee>> Post(Employee employee)
        {
            try
            {
                if(employee==null)
                {
                    return BadRequest(new {Response="Employee Not Passed or Created"});
                }
                var emp = await employeeRepository.AddEmployee(employee);
                return CreatedAtAction(nameof(GetEmployee), new { id = emp.EmployeeId }, emp);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error Creating Employee in database");
            }
        }

        // PUT api/<EmployeeController>/5
        [HttpPut("{id:int}")]
        public async Task<ActionResult<Employee>> Put(int id, Employee employee)
        {
            try
            { 
                if(employee.EmployeeId!=id)
                {
                    return BadRequest(new { Response = "Employee Id Mismatch" });
                }
                var empToUpdate=await employeeRepository.GetEmployeeById(id);
                if(empToUpdate==null)
                {
                    return NotFound($"Employee with Employee Id ={id} not found");
                }
                return await employeeRepository.UpdateEmployee(employee);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error Updating Employee Record in database");
            }
        }

        // DELETE api/<EmployeeController>/5
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var emp = await employeeRepository.GetEmployeeById(id);
                if(emp==null)
                {
                    return NotFound($"Employee with Id={id} not found");
                }
                await employeeRepository.DeleteEmployee(id);
                return Ok($"Employee with Id={id} has been Deleted Successfully");
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error Deleting Employee Record in database");
            }
        }
    }
}
