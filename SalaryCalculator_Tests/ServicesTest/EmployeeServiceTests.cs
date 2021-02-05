using AutoMapper;
using NUnit.Framework;
using SalaryCalculator_Common.Enums;
using SalaryCalculator_Common.Models;
using SalaryCalculator_Core.Factories;
using SalaryCalculator_Core.Services;
using SalaryCalculator_Data.Entities;
using SalaryCalculator_Data.EntityProfiles;
using SalaryCalculator_Data.Repositories;
using System;
using System.Collections.Generic;
using System.Data;

namespace Sprout.SalaryCalculator_Tests.ServicesTest
{
    class EmployeeServiceTests
    {
        private IEmployeeRepository _mockEmployeeRepository;
        private IEmployeeFactory _employeeFactory;
        private IMapper _mapper;
        private List<Employee> _fakeEmployeesInMemory;

        [SetUp]
        public void Setup()
        {
            _fakeEmployeesInMemory = new List<Employee>
            {
                new Employee()
                {
                    Name = "Bob Regular",
                    BirthDate = DateTime.Parse("01/01/2021"),
                    TIN = "111111111",
                    EmployeeTypeId = (int) EmployeeContractType.RegularEmployee
                },
                new Employee()
                {
                    Name = "Dylan Regular",
                    BirthDate = DateTime.Parse("01/01/2021"),
                    TIN = "222222222",
                    EmployeeTypeId = (int) EmployeeContractType.ContractualEmployee
                },
                 new Employee()
                {
                    Name = "Martin Contractor",
                    BirthDate = DateTime.Parse("01/01/2021"),
                    TIN = "333333333",
                    EmployeeTypeId = (int) EmployeeContractType.RegularEmployee
                }
            };

            _employeeFactory = new EmployeeFactory();
            _mockEmployeeRepository = new EmployeeRepository(_fakeEmployeesInMemory);

            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<EmployeeProfile>();
            });
            _mapper = new Mapper(configuration);
        }

        [Test]
        public void AddEmployee_AddOneEmployee_ShouldReturnAddedEmployeeNotNull()
        {
            //Arrange

            EmployeeModel employeeModel = new EmployeeModel();
            employeeModel.Name = "Naruto Uzumaki";
            employeeModel.BirthDate = DateTime.Parse("01-01-2021");
            employeeModel.TIN = "123456789";
            employeeModel.EmployeeTypeId = (int)EmployeeContractType.RegularEmployee;

            EmployeeService employeeService = new EmployeeService(_mockEmployeeRepository, _mapper, _employeeFactory);

            //Act
            var addedEmployee = employeeService.Add(employeeModel);

            //Assert
            Assert.IsTrue(addedEmployee != null);

        }

        [Test]
        public void AddEmployee_AddEmployeeThatAlreadyExists_ShouldThrowDataExceptionAndNeverHappened()
        {
            Employee existingEmployee = _fakeEmployeesInMemory[0];
            EmployeeService employeeService = new EmployeeService(_mockEmployeeRepository, _mapper, _employeeFactory);

            //Act
            var employee = _mapper.Map<EmployeeModel>(existingEmployee);
            var ex = Assert.Throws<DataException>(() => employeeService.Add(employee));

            //Assert
            Assert.That(ex.Message.Equals("Employee Id already exists"));

        }

        [Test]
        public void GetById_ExistingEmployee_ShouldReturnEmployeeModelSameId()
        {

            EmployeeService employeeService = new EmployeeService(_mockEmployeeRepository, _mapper, _employeeFactory);
            Employee existingEmployee = _fakeEmployeesInMemory[0];
            //Act
            var employeeActual = employeeService.GetEmployee(existingEmployee.Id);

            //Assert
            Assert.That(employeeActual.Id == existingEmployee.Id);

        }

        [Test]
        public void GetById_NotExistingEmployee_ShouldReturnNullEmployee()
        {
            //Arrange
            Guid nonExistentId = Guid.NewGuid();
            EmployeeService employeeService = new EmployeeService(_mockEmployeeRepository, _mapper, _employeeFactory);

            //Act
            var employeeModel = employeeService.GetEmployee(nonExistentId);

            //Assert
            Assert.IsNull(employeeModel);

        }

        [Test]
        public void GetEmployees_NotEmptyList_ShouldReturnNotNull()
        {
            //Arrange
            EmployeeService employeeService = new EmployeeService(_mockEmployeeRepository, _mapper, _employeeFactory);

            //Act

            var employees = employeeService.GetEmployees();

            //Assert
            Assert.IsNotNull(employees);

        }

        [Test]
        public void GetEmployees_EmptyList_ShouldReturnEmpty()
        {
            //Arrange
            List<Employee> emptyEmployee = new List<Employee>();
            _mockEmployeeRepository = new EmployeeRepository(emptyEmployee);
            EmployeeService employeeService = new EmployeeService(_mockEmployeeRepository, _mapper, _employeeFactory);

            //Act

            var employees = employeeService.GetEmployees();

            //Assert
            Assert.IsEmpty(employees);

        }

        [Test]
        public void GetSalary_RegularEmployeeNoAbsent20kMonthly_ShouldReturnEqual()
        {
            //Arrange
            var existingEmployee = _fakeEmployeesInMemory[0];
            EmployeeService employeeService = new EmployeeService(_mockEmployeeRepository, _mapper, _employeeFactory);

            //Act
            var regularEmployeeModel = employeeService.GetEmployee(existingEmployee.Id,
                new
                {
                    MonthlySalary = 20000,
                    DaysAbsent = 1
                });
            var salary = regularEmployeeModel.GetSalary();

            //Assert
            Assert.AreEqual("16690.91", salary);

        }

        [Test]
        public void GetSalary_ContractualEmployeeSalary_ShouldReturn7750()
        {
            //Arrange
            var existingEmployee = _fakeEmployeesInMemory[1];
            EmployeeService employeeService = new EmployeeService(_mockEmployeeRepository, _mapper, _employeeFactory);


            //Act
            var employee = employeeService.GetEmployee(existingEmployee.Id,
               new
               {
                   RatePerDay = 500,
                   DaysWorked = 15.5
               });

            var salary = employee.GetSalary();

            //Assert
            Assert.AreEqual("7750.00", salary);

        }

        [Test]
        public void GetSalary_RegularZeroSalary_ShouldReturnZeroWithTwoDecimalPlace()
        {
            //Arrange
            var existingEmployee = _fakeEmployeesInMemory[1];
            EmployeeService employeeService = new EmployeeService(_mockEmployeeRepository, _mapper, _employeeFactory);


            //Act
            var employee = employeeService.GetEmployee(existingEmployee.Id,
               new
               {
                   MonthlySalary = 0,
                   DaysAbsent = 0
               });

            var salary = employee.GetSalary();

            //Assert
            Assert.AreEqual("0.00", salary);

        }

        [Test]
        public void GetSalary_ContractualZeroSalary_ShouldReturnZeroWithTwoDecimalPlace()
        {
            //Arrange
            var existingEmployee = _fakeEmployeesInMemory[1];
            EmployeeService employeeService = new EmployeeService(_mockEmployeeRepository, _mapper, _employeeFactory);


            //Act
            var employee = employeeService.GetEmployee(existingEmployee.Id,
               new
               {
                   RatePerDay = 0,
                   DaysWorked = 0
               });

            var salary = employee.GetSalary();

            //Assert
            Assert.AreEqual("0.00", salary);

        }

    }
}