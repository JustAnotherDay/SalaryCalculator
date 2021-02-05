using SalaryCalculator_Common.Enums;
using SalaryCalculator_Common.Extensions;
using System;
using System.ComponentModel.DataAnnotations;

namespace SalaryCalculator_Common.Models
{
    public class RegularEmployeeModel : IEmployeeModel
    {
        public RegularEmployeeModel()
        {
            EmployeeTypeName = Enum.GetName(typeof(EmployeeContractType), (int)EmployeeContractType.RegularEmployee);
            TaxInPercent = 12;

        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public string TIN { get; set; }
        public int EmployeeTypeId { get; set; }
        public string EmployeeTypeName { get; }

        //Custom Properties
        [Range(0.0, Double.MaxValue, ErrorMessage = "The field {0} must be greater than {1}.")]
        public double MonthlySalary { get; set; }

        [Range(0.0, Double.MaxValue, ErrorMessage = "The field {0} must be greater than {1}.")]
        public double DaysAbsent { get; set; }
        private double TaxInPercent { get; }

        public string GetSalary ()
        {
            double taxInDecimal = TaxInPercent / 100;
            double absentDaysDeduction = (MonthlySalary / 22) * DaysAbsent;
            double taxDeduction = MonthlySalary * taxInDecimal;
            double salary = MonthlySalary - absentDaysDeduction - taxDeduction;
            string formattedSalary = salary.RoundAndFormatToTwoDecimalPlace();
            return formattedSalary;
        }

    }
}
