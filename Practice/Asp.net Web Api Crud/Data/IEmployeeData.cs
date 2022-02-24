using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Asp.net_Web_Api_Crud.Models;

namespace Asp.net_Web_Api_Crud.Data
{
    public interface IEmployeeData
    {
        List<Employee> GetEmployees();
        Employee GetEmployee(Guid id);
        Employee AddEmployee(Employee employee);
        void DeleteEmployee(Employee employee);
        Employee EditEmployee(Employee employee);

    }
}
