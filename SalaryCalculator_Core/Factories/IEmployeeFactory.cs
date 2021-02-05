using SalaryCalculator_Common.Models;

namespace SalaryCalculator_Core.Factories
{
    public interface IEmployeeFactory
    {
        public IEmployeeModel CreateEmployee(int employeeTypeId);
        public IEmployeeModel CreateEmployee(int employeeTypeId, object modelCustomProperty);
    }
}
