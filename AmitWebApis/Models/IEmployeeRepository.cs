using AmitWebApis.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace AmitWebApis.Models
{
    public interface IEmployeeRepository
    {
        public Task<IEnumerable<Employee>> GetEmployees();
        public Task<IEnumerable<Employee>> SearchEmployee(string name, Gender? gender);
        public Task<Employee> GetEmployeeById(int id);
        public Task<Employee> GetEmployeeByEmail(string email);
        public Task<Employee> AddEmployee(Employee employee);
        public Task<Employee> UpdateEmployee(Employee employee);
        public Task<Employee> DeleteEmployee(int id);

    }
}

//IEmployeeRepository interface supports the following operations
//•Search employees by name and gender
//•Get all the employees
//•Get a single employee by id
//•Get an employee by their email address
//•Add a new employee
//•Updat an employee
//•Delete an employee
