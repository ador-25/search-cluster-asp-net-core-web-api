using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Asp.net_Web_Api_Crud.Data;
using Asp.net_Web_Api_Crud.Models;

namespace Asp.net_Web_Api_Crud.Controllers
{
    [ApiController]
    
    public class EmployeesController : ControllerBase
    {
        private IEmployeeData _employeeData;
        public EmployeesController(IEmployeeData employeeData)
        {
            _employeeData = employeeData;
        }
        [HttpGet]
        [Route("api/[controller]")]
        public IActionResult GetEmployees()
        {
            return Ok(_employeeData.GetEmployees());
        }
        [HttpGet]
        [Route("api/[controller]/{id}")]
        public IActionResult GetEmployee(Guid id)
        {
            var employee = _employeeData.GetEmployee(id);
            return employee != null ? Ok(_employeeData.GetEmployee(id)) :NotFound("Employee not found");
        }



        [HttpPost]
        [Route("api/[controller]/employee")]
        public IActionResult AddEmployee([FromQuery] Employee employee)
        {
            _employeeData.AddEmployee(employee);
            return Created(HttpContext.Request.Scheme + "://" + HttpContext.Request.Host + HttpContext.Request.Path+"/"+ employee.Id,employee);
        }

        [HttpDelete]
        [Route("api/[controller]/{id}")]
        public IActionResult DeleteEmployee(Guid id)
        {
            var emp = _employeeData.GetEmployee(id);
            if(emp!= null)
            {
                _employeeData.DeleteEmployee(emp);
                return Ok();
            }
            else
            {
                return NotFound("Employee not found");
            }
        }


        // problem here
        [HttpPatch]
        [Route("api/[controller]/{id}/employee")]
        public IActionResult EditEmployee(Guid id,[FromQuery]Employee employee) // getting name from query and id from route
        {
            var existingEmployee = _employeeData.GetEmployee(id);
            if (existingEmployee != null)
            {
                employee.Id = existingEmployee.Id;
                _employeeData.EditEmployee(employee);   
                return Ok();
            }
            else
            {
                return NotFound("Employee not found");
            }
        }

    }
}
