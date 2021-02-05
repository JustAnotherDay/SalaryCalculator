using SalaryCalculator_Common.Enums;
using SalaryCalculator_Common.Extensions;
using System;
using System.ComponentModel.DataAnnotations;

namespace SalaryCalculator_Common.Models
{
    public class ContractualEmployeeModel : IEmployeeModel
    {
        public ContractualEmployeeModel()
        {
            EmployeeTypeName = Enum.GetName(typeof(EmployeeContractType), (int) EmployeeContractType.ContractualEmployee);
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public string TIN { get; set; }
        public int EmployeeTypeId { get; set; }
        public string EmployeeTypeName { get; }

        [Range(0.0, Double.MaxValue, ErrorMessage = "The field {0} must be greater than {1}.")]
        public double RatePerDay { get; set; }
        [Range(0.0, Double.MaxValue, ErrorMessage = "The field {0} must be greater than {1}.")]
        public double DaysWorked { get; set; }

        public string GetSalary()
        {
            double salary = RatePerDay * DaysWorked;
            string formattedSalary = salary.RoundAndFormatToTwoDecimalPlace();
            return formattedSalary;
        }

    }
}
