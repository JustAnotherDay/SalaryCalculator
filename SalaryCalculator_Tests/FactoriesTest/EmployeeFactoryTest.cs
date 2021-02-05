using NUnit.Framework;
using SalaryCalculator_Common.Enums;
using SalaryCalculator_Common.Models;
using SalaryCalculator_Core.Factories;

namespace Sprout.SalaryCalculator_Tests.FactoriesTest
{
    class EmployeeFactoryTest
    {
        [Test]
        public void CreateEmployee_EmployeeTypeIdZero_ShouldReturnRegularEmployee()
        {
            //Arrange
            var employeeType = (int) EmployeeContractType.RegularEmployee;
            var employeeFactory = new EmployeeFactory();

            //Act
            IEmployeeModel employee = employeeFactory.CreateEmployee(employeeType);

            //Assert
            Assert.IsInstanceOf<RegularEmployeeModel>(employee);
        }

        [Test]
        public void CreateEmployee_EmployeeTypeIdOne_ShouldReturnContractualEmployee()
        {
            //Arrange
            var employeeType = (int)EmployeeContractType.ContractualEmployee;
            var employeeFactory = new EmployeeFactory();
            //Act
            IEmployeeModel employee = employeeFactory.CreateEmployee(employeeType);

            //Assert
            Assert.IsInstanceOf<ContractualEmployeeModel>(employee);
        }

        [Test]
        public void CreateEmployee_EmployeeTypeIdInvalid_ShouldReturnNull()
        {
            //Arrange
            var employeeType = 99;
            var employeeFactory = new EmployeeFactory();

            //Act
            IEmployeeModel employee = employeeFactory.CreateEmployee(employeeType);

            //Assert
            Assert.IsNull(employee);
        }
    }
}