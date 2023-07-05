using Microsoft.EntityFrameworkCore;
namespace AmitWebApis.Models
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext appDbContext;

        public EmployeeRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        public async Task<Employee> AddEmployee(Employee employee)
        {
            if(employee.Department!=null)
            {
                appDbContext.Entry(employee.Department).State= Microsoft.EntityFrameworkCore.EntityState.Unchanged;
            }
            var emp = await appDbContext.Employees.AddAsync(employee);
            await appDbContext.SaveChangesAsync();
            return employee;
        }

        public async Task<Employee> DeleteEmployee(int id)
        {
            var emp = await appDbContext.Employees.FirstOrDefaultAsync(x => x.EmployeeId == id);
            if(emp!=null)
            {
                appDbContext.Employees.Remove(emp);
                await appDbContext.SaveChangesAsync();
                return emp;
            }
            return null;
        }

        public async Task<Employee> GetEmployeeByEmail(string email)
        {
            return await appDbContext.Employees.FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task<Employee> GetEmployeeById(int id)
        {
            return await appDbContext.Employees.FirstOrDefaultAsync(x => x.EmployeeId == id);
        }

        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            return await appDbContext.Employees.ToListAsync();
        }

        public async Task<IEnumerable<Employee>> SearchEmployee(string name, Gender? gender)
        {
            IQueryable<Employee> query = appDbContext.Employees;
            if(!(string.IsNullOrEmpty(name)))
            {
                query = query.Where(x => x.FirstName.Contains(name) || x.LastName.Contains(name));
            }
            if(gender!=null)
            {
                query = query.Where(x => x.Sex == gender);
            }
            return await query.ToListAsync();
            
        }

        public async Task<Employee> UpdateEmployee(Employee employee)
        {
            var emp = await appDbContext.Employees.Include(x=>x.Department).FirstOrDefaultAsync(x => x.EmployeeId == employee.EmployeeId);
            if(emp != null)
            {
                emp.FirstName=employee.FirstName;
                emp.LastName = employee.LastName;
                emp.Sex=employee.Sex;
                emp.Email = employee.Email;
                emp.DateOfBirth = employee.DateOfBirth;
                if(employee.DepartmentId!=0)
                {
                    emp.DepartmentId = employee.DepartmentId;
                }
                if(employee.Department!=null)
                {
                    emp.DepartmentId=employee.Department.DepartmentId;
                }
                emp.PhotoPath = employee.PhotoPath;
                await appDbContext.SaveChangesAsync();
                return await GetEmployeeById(emp.EmployeeId);
            }
            return null;
        }
    }
}
