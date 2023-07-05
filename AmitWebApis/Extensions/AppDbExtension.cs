using AmitWebApis.Models;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace AmitWebApis.Extensions
{
    public static class AppDbExtension
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            Department d1 = new Department()
            {
                DepartmentId = 10,
                DepartmentName =".Net"
            };
            Department d2 = new Department()
            {
                DepartmentId = 20,
                DepartmentName =
            "HR"
            };
            modelBuilder.Entity<Department>().HasData(d1);
            modelBuilder.Entity<Department>().HasData(d2);

            Employee e1 = new Employee()
            {
                EmployeeId = 1001,
                FirstName = "Rai",
                LastName = "Singh",
                Email = "rai.verma@sircltech.com",
                DateOfBirth = new DateTime(1985, 09, 02),
                Sex = Gender.Male,
                PhotoPath = "/images/rai.jpg",
                DepartmentId = 10
            };
            Employee e2 = new Employee()
            {
                EmployeeId = 1002,
                FirstName = "Naresh",
                LastName = "Verma",
                Email = "naresh@sircltech.com",
                DateOfBirth = new DateTime(1986, 07, 07),
                Sex = Gender.Male,
                PhotoPath = "/images/naresh.jpg",
                DepartmentId = 20
            };
            modelBuilder.Entity<Employee>().HasData(e1);
            modelBuilder.Entity<Employee>().HasData(e2);

        }

    }
}
