using System;
using System.ComponentModel.DataAnnotations;

namespace SalaryCalculator_Common.Models
{
    public class EmployeeModel
    {
        public EmployeeModel()
        {
            
        }

        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        //[Range(typeof(DateTime), "01/01/1900", "01/01/2100", ErrorMessage = "Birthdate invalid.")]
        [Required]
        public DateTime BirthDate { get; set; }

        [Required]
        [RegularExpression(@"^\d{9}$", ErrorMessage = "TIN invalid format. 9 digit")]
        public string TIN { get; set; }

        [Required]
        public int EmployeeTypeId { get; set; }

        public string EmployeeTypeName { get; set; }

    }
}
