using SalaryCalculator_Common.Enums;
using SalaryCalculator_Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SalaryCalculator_Data.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private static List<Employee> _employees;

        public EmployeeRepository(List<Employee> employee)
        {
            _employees = employee;
        }

        public EmployeeRepository()
        {
            _employees = new List<Employee>();
            _employees.Add(new Employee
            {
                Name = "Sasuke Uzumaki",
                BirthDate = DateTime.Parse("05/05/2021"),
                TIN = "999999999",
                EmployeeTypeId = (int)EmployeeContractType.RegularEmployee
            });
            _employees.Add(new Employee
            {
                Name = "Bob Uzumaki",
                BirthDate = DateTime.Parse("05/05/2021"),
                TIN = "888888888",
                EmployeeTypeId = (int)EmployeeContractType.ContractualEmployee
            });
        }

        private int EmployeesCount()
        {
            return _employees.Count();
        }

        public bool IsEmployeeExists(Guid id)
        {
            return _employees.Any(emp => emp.Id == id);
        }

        public void Add(Employee employee)
        {
            _employees.Add(employee);
        }

        public Employee GetById(Guid id)
        {
            var employee = _employees
                .FirstOrDefault(emp => emp.Id == id);
            return employee;
        }

        public IReadOnlyCollection<Employee> GetAll()
        {
            return _employees.ToList();
        }
    }
}
