using System;
using System.Collections.Generic;
using System.Text;

namespace SalaryCalculator_Common.Models
{
    public class GetSalaryModel
    {
        public Guid Id { get; set; }
        public double MonthlySalary { get; set; }
        public double DaysAbsent { get; set; }
        public double RatePerDay { get; set; }
        public double DaysWorked { get; set; }
    }
}
