using SalaryCalculator_Common.Enums;
using System;

namespace SalaryCalculator_Data.Entities
{
    public class Employee
    {
        public Employee()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; }

        public string Name { get; set; }

        public DateTime BirthDate { get; set; }

        public string TIN { get; set; }

        public int EmployeeTypeId { get; set; }

        public string EmployeeTypeName { get { return Enum.GetName(typeof(EmployeeContractType), EmployeeTypeId); } }
    }
}
