using Microsoft.EntityFrameworkCore;

namespace AmitWebApis.Models
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly AppDbContext appDbContext;

        public DepartmentRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        public async Task<Department> AddDepartment(Department department)
        {
            
                await appDbContext.Department.AddAsync(department);
                await appDbContext.SaveChangesAsync();
                return department;
        
        }

        public async Task<Department> DeleteDepartment(int id)
        {
            var dept = await appDbContext.Department.FirstOrDefaultAsync(x => x.DepartmentId == id);
            if(dept != null)
            {
                appDbContext.Department.Remove(dept);
                await appDbContext.SaveChangesAsync();
                return dept;
            }
            return null;
        }

        public async Task<Department> GetDepartmentById(int id)
        {
            return await appDbContext.Department.FirstOrDefaultAsync(x => x.DepartmentId == id);
        }

        public async Task<IEnumerable<Department>> GetDepartments()
        {
            return await appDbContext.Department.ToListAsync();
        }

        public async Task<Department> UpdateDepartment(Department department)
        {
            var dept=await appDbContext.Department.FirstOrDefaultAsync(x=>x.DepartmentId==department.DepartmentId);
            if(dept!=null)
            {
                
                dept.DepartmentName = department.DepartmentName;
                await appDbContext.SaveChangesAsync();
                return dept;
            }
            return null;
        }
    }
}
