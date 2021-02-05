using AutoMapper;
using SalaryCalculator_Common.Models;
using SalaryCalculator_Data.Entities;

namespace SalaryCalculator_Data.EntityProfiles
{
	public class EmployeeProfile : Profile
    {
		public EmployeeProfile()
		{
			this.CreateMap<Employee, EmployeeModel>();
			this.CreateMap<EmployeeModel, Employee>();
		}
	}
}
