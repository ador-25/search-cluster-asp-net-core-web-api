using Asp.net_Web_Api_Crud.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asp.net_Web_Api_Crud.Data
{
    public class MockEmployeeData : IEmployeeData
    {
        private List<Employee> employees = new List<Employee>()
        {
            new Employee()
            {
                Id =Guid.NewGuid(),
                Name= "Ador"
            },
            new Employee()
            {
                Id =Guid.NewGuid(),
                Name= "Nabil"
            }
        };
        public Employee AddEmployee(Employee employee)
        {
            employee.Id = Guid.NewGuid();
            employees.Add(employee);
            return employee;
        }

        public void DeleteEmployee(Employee employee)
        {
            employees.Remove(employee);
        }

        public Employee EditEmployee(Employee employee)
        {
            var existingEmployee = GetEmployee(employee.Id);
            existingEmployee.Name = employee.Name;
            return existingEmployee;
        }

        public Employee GetEmployee(Guid id)
        {
            return employees.SingleOrDefault(x => x.Id == id);
        }

        public List<Employee> GetEmployees()
        {
            return employees;
        }
    }
}
