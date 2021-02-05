using SalaryCalculator_Data.Entities;
using System;
using System.Collections.Generic;

namespace SalaryCalculator_Data.Repositories
{
    public interface IEmployeeRepository
    {
        public IReadOnlyCollection<Employee> GetAll();
        public Employee GetById(Guid id);
        public void Add(Employee employee);
        bool IsEmployeeExists(Guid id);
    }
}
