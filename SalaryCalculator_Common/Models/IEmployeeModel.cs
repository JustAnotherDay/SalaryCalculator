using System;

namespace SalaryCalculator_Common.Models
{
    public interface IEmployeeModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public string TIN { get; set; }
        public int EmployeeTypeId { get; set; }
        public string EmployeeTypeName { get; }
        public string GetSalary();
    }
}
