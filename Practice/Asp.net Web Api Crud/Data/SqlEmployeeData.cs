using Asp.net_Web_Api_Crud.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Asp.net_Web_Api_Crud.Data;
namespace Asp.net_Web_Api_Crud.Data
{
    public class SqlEmployeeData : IEmployeeData
    {
        private EmployeeContext _employeeContext;
        public SqlEmployeeData(EmployeeContext employeeContext)
        {
            _employeeContext = employeeContext;
        }
        public Employee AddEmployee(Employee employee)
        {
            employee.Id = new Guid();
            _employeeContext.Employees.Add(employee);
            _employeeContext.SaveChanges();
            return employee;
        }

        public void DeleteEmployee(Employee employee)
        {
            throw new NotImplementedException();
        }

        public Employee EditEmployee(Employee employee)
        {
            throw new NotImplementedException();
        }

        public Employee GetEmployee(Guid id)
        {

            var employee = _employeeContext.Employees.Find(id);
            return employee;
           
            // return _employeeContext.Employees.SingleOrDefault(x => x.Id == id); Also another way
        }

        public List<Employee> GetEmployees()
        {
            return _employeeContext.Employees.ToList();
        }
    }
}
