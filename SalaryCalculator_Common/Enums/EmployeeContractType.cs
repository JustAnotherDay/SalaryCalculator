using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Text;

namespace SalaryCalculator_Common.Enums
{
    public enum EmployeeContractType
    {
        [Description("Regular Employee")]
        RegularEmployee = 0,
        [Description("Contractual Employee")]
        ContractualEmployee = 1,
    }
}
