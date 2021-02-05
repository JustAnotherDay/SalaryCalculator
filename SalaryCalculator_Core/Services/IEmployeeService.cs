using SalaryCalculator_Common.Models;
using System;
using System.Collections.Generic;

namespace SalaryCalculator_Core.Services
{
    public interface IEmployeeService
    {
        public IReadOnlyCollection<EmployeeModel> GetEmployees();
        public IEmployeeModel GetEmployee(Guid id);
        public IEmployeeModel GetEmployee(Guid id, object model);
        public EmployeeModel Add(EmployeeModel employee);
    }
}
