using SalaryCalculator_Common.Enums;
using SalaryCalculator_Common.Models;
using System.Linq;
using System.Reflection;

namespace SalaryCalculator_Core.Factories
{
    public class EmployeeFactory : IEmployeeFactory
    {
        public IEmployeeModel CreateEmployee(int employeeTypeId)
        {
            IEmployeeModel employee = null;

            if((int)EmployeeContractType.RegularEmployee == employeeTypeId)
            {
                employee = new RegularEmployeeModel();
            }
            else if ((int)EmployeeContractType.ContractualEmployee == employeeTypeId)
            {
                employee = new ContractualEmployeeModel();
            }

            return employee;
        }

        public IEmployeeModel CreateEmployee(int employeeTypeId, object modelCustomProperty)
        {
            IEmployeeModel employee = null;

            if ((int)EmployeeContractType.RegularEmployee == employeeTypeId)
            {
                var emp = new RegularEmployeeModel();
                
                if(modelCustomProperty != null)
                {
                    PropertyInfo[] props = modelCustomProperty.GetType().GetProperties();
                    PropertyInfo[] tProps = emp.GetType().GetProperties();

                    foreach (var prop in props)
                    {
                        var upperPropName = prop.Name.ToUpper();
                        var foundProperty = tProps.FirstOrDefault(p => p.Name.ToUpper() == upperPropName);
                        foundProperty?.SetValue(emp, prop.GetValue(modelCustomProperty));
                    }
                }
               
                return emp;
            }
            else if ((int)EmployeeContractType.ContractualEmployee == employeeTypeId)
            {
                var emp = new ContractualEmployeeModel();
                if (modelCustomProperty != null)
                {
                    PropertyInfo[] props = modelCustomProperty.GetType().GetProperties();
                    PropertyInfo[] tProps = emp.GetType().GetProperties();

                    foreach (var prop in props)
                    {
                        var upperPropName = prop.Name.ToUpper();
                        var foundProperty = tProps.FirstOrDefault(p => p.Name.ToUpper() == upperPropName);
                        foundProperty?.SetValue(emp, prop.GetValue(modelCustomProperty));
                    }
                }
                return emp;
            }

            return employee;
        }
    }
}


//using Sprout.Common.Enums;
//using Sprout.SalaryCalculatorCore.Models;
//using System.Linq;
//using System.Reflection;

//namespace SalaryCalculator_Core.Factories
//{
//    public class EmployeeFactory : IEmployeeFactory
//    {
//        public IEmployeeModel CreateEmployee(int employeeTypeId, object modelCustomProperty)
//        {

//            if ((int)EmployeeContractType.RegularEmployee == employeeTypeId)
//            {
//                var employee = new RegularEmployeeModel();
//                PropertyInfo[] props = modelCustomProperty.GetType().GetProperties();
//                PropertyInfo[] tProps = employee.GetType().GetProperties();

//                foreach (var prop in props)
//                {
//                    var upperPropName = prop.Name.ToUpper();
//                    var foundProperty = tProps.FirstOrDefault(p => p.Name.ToUpper() == upperPropName);
//                    foundProperty?.SetValue(employee, prop.GetValue(modelCustomProperty));
//                }
//                return employee;
//            }
//            else if ((int)EmployeeContractType.ContractualEmployee == employeeTypeId)
//            {
//                var employee = new ContractualEmployeeModel();
//                PropertyInfo[] props = modelCustomProperty.GetType().GetProperties();
//                PropertyInfo[] tProps = employee.GetType().GetProperties();

//                foreach (var prop in props)
//                {
//                    var upperPropName = prop.Name.ToUpper();
//                    var foundProperty = tProps.FirstOrDefault(p => p.Name.ToUpper() == upperPropName);
//                    foundProperty?.SetValue(employee, prop.GetValue(modelCustomProperty));
//                }

//                return employee;
//            }

//            return null;
//        }
//    }
//}

