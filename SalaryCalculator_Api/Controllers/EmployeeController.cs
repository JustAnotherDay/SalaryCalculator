using Microsoft.AspNetCore.Mvc;
using SalaryCalculator_Common.Models;
using SalaryCalculator_Core.Services;
using System;

namespace SalaryCalculator_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        [Route("")]
        public IActionResult GetAllEmployee()
        {
            return Ok(_employeeService.GetEmployees());
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetEmployeeById(Guid id)
        {
            return Ok(_employeeService.GetEmployee(id));
        }

        [HttpPost]
        [Route("add")]
        public IActionResult AddEmployee([FromBody] EmployeeModel employeeModel)
        {
            try
            {
                _employeeService.Add(employeeModel);
                return Ok();
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        [HttpPost]
        [Route("salary")]
        public IActionResult GetSalary([FromBody] GetSalaryModel getSalaryModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var employee = _employeeService.GetEmployee(getSalaryModel.Id, getSalaryModel);
                    var salary = employee.GetSalary();
                    return Ok(new { salary });
                }

                return BadRequest();
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
