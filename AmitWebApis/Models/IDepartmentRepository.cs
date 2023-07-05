namespace AmitWebApis.Models
{
    public interface IDepartmentRepository
    {
        public Task<IEnumerable<Department>> GetDepartments();
        public Task<Department> GetDepartmentById(int id);
        public Task<Department> AddDepartment(Department department);
        public Task<Department> UpdateDepartment(Department department);
        public Task<Department> DeleteDepartment(int id);
    }
}
