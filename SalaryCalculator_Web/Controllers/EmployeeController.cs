using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SalaryCalculator_Common.Enums;
using SalaryCalculator_Common.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SalaryCalculator_Web.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string apiEndPoint;

        public EmployeeController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
            apiEndPoint = _configuration.GetValue<string>("WebAPIBaseUrl");
        }

        // GET: Employees
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(apiEndPoint);
            var response = await client.GetAsync("/api/employee");
            var result = await response.Content.ReadAsStringAsync();
            List<EmployeeModel> employees = JsonConvert.DeserializeObject<List<EmployeeModel>>(result);

            return View(employees);

        }

        //// GET: Employees/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(apiEndPoint);
            var response = await client.GetAsync($"/api/employee/{id}");
             var result = await response.Content.ReadAsStringAsync();

            if (result == "")
                return View("NotFound");

            //REFACTOR
            var employee = JsonConvert.DeserializeObject<EmployeeModel>(result);
            if(employee.EmployeeTypeId == (int) EmployeeContractType.RegularEmployee)
            {
                var employeeModel = JsonConvert.DeserializeObject<RegularEmployeeModel>(result);
                return View("RegularEmployee", employeeModel);
            }
            else
            {
                var employeeModel = JsonConvert.DeserializeObject<ContractualEmployeeModel>(result);
                return View("ContractualEmployee", employeeModel);
            }
        }

        // GET: Employees/Create
        public IActionResult Create()
        {
            return View();
        }

        //// POST: Employees/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmployeeModel employeeModel)
        {
            try
            {
                string url = apiEndPoint + "/employee/add/";
                var employee = new StringContent(
                    JsonConvert.SerializeObject(employeeModel),
                    Encoding.UTF8,
                    "application/json");

                var client = _httpClientFactory.CreateClient();
                client.BaseAddress = new Uri(apiEndPoint);

                var resposnse = await client.PostAsync($"/api/employee/add", employee);

                if (resposnse.IsSuccessStatusCode)
                    return Redirect("/");


                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GetSalary(GetSalaryModel employeeModel)
        {
            try
            {
                string url = apiEndPoint + "api/employee/salary/";
                var employee = new StringContent(
                    JsonConvert.SerializeObject(employeeModel),
                    Encoding.UTF8,
                    "application/json");

                var client = _httpClientFactory.CreateClient();
                client.BaseAddress = new Uri(apiEndPoint);

                var response = await client.PostAsync(url, employee);
                var result = await response.Content.ReadAsStringAsync();
                var resultObject = JsonConvert.DeserializeObject(result);
                if (response.IsSuccessStatusCode)
                    ViewBag.Salary = "984";

                
                return PartialView("_RegularEmployee.cshtml", employeeModel);
            }
            catch
            {
                return View();
            }
        }

        //// GET: Employees/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: Employees/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add update logic here

        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: Employees/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: Employees/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add delete logic here

        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}