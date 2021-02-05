using AutoMapper;
using SalaryCalculator_Common.Models;
using SalaryCalculator_Core.Factories;
using SalaryCalculator_Data.Entities;
using SalaryCalculator_Data.Repositories;
using System;
using System.Collections.Generic;
using System.Data;

namespace SalaryCalculator_Core.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;
        private readonly IEmployeeFactory _employeeFactory;

        public EmployeeService(IEmployeeRepository employeeRepository, IMapper mapper, IEmployeeFactory employeeFactory)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
            _employeeFactory = employeeFactory;
        }

        public EmployeeModel Add(EmployeeModel employeeModel)
        {
            if (!_employeeRepository.IsEmployeeExists(employeeModel.Id))
            {
                var employee = _mapper.Map<Employee>(employeeModel);
                _employeeRepository.Add(employee);

                return _mapper.Map<EmployeeModel>(employee);
            }
            else
                throw new DataException("Employee Id already exists");
        }

        public IEmployeeModel GetEmployee(Guid id)
        {
            var employee = _employeeRepository.GetById(id);
            if (employee != null)
                return MapEntityToEmployeeType(employee, null);
            else return null;
        }
        public IEmployeeModel GetEmployee(Guid id, object model)
        {
            var employee = _employeeRepository.GetById(id);
            if (employee != null)
                return MapEntityToEmployeeType(employee, model);
            else return null;
        }

        private IEmployeeModel MapEntityToEmployeeType(Employee employee, object model)
        {
            var employeeType = _employeeFactory.CreateEmployee(employee.EmployeeTypeId, model);
            employeeType.Id = employee.Id;
            employeeType.Name = employee.Name;
            employeeType.BirthDate = employee.BirthDate;
            employeeType.TIN = employee.TIN;
            employeeType.EmployeeTypeId = employee.EmployeeTypeId;

            return employeeType;
        }

        public IEmployeeModel MapEntityToEmployeeType2(IEmployeeModel employee)
        {
            var employeeType = _employeeFactory.CreateEmployee(employee.EmployeeTypeId);
            employeeType.Id = employee.Id;
            employeeType.Name = employee.Name;
            employeeType.BirthDate = employee.BirthDate;
            employeeType.TIN = employee.TIN;
            employeeType.EmployeeTypeId = employee.EmployeeTypeId;

            return employeeType;
        }

        public IReadOnlyCollection<EmployeeModel> GetEmployees()
        {
            var employees = _employeeRepository.GetAll();

            return _mapper.Map<EmployeeModel[]>(employees);
        }

    }
}
