using CodeChallenge.DTOs;
using CodeChallenge.Models;
using CodeCodeChallenge.Tests.Integration.Extensions;
using CodeCodeChallenge.Tests.Integration.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CodeChallenge.Tests.Integration
{
    [TestClass]
    public class CompensationControllerTests
    {
        private static HttpClient _client;
        private static TestServer _server;

        [ClassInitialize]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "<Pending>")]
        public static void InitializeClass(TestContext context)
        {
            _server = new TestServer();
            _client = _server.NewClient();
        }

        [ClassCleanup]
        public static void CleanUpTest()
        {
            _client.Dispose();
            _server.Dispose();
        }

        [TestMethod]
        public static void CreateCompensation_Returns_Created()
        {
            /*
             * the direct reports list in the object makes it a little difficult to just create a new 
             * employee object without calling the reporting structure code or creating a massive array instantiation
             * of different employees
            */
            var employeeId = "16a596ae-edd3-4847-99fe-c4518e82c86f";
            var fName = "John";
            var lName = "Lennon";
            var position = "Development Manager";
            var department = "Engineering";


            var compensation = new CompensationDto()
            {
                EmployeeId = employeeId,
                Salary = 123456.78,
                EffectiveDate = DateTime.Now,
            };

            var requestContent = new JsonSerialization().ToJson(compensation);
            var postRequest = _client.PostAsync("api/compensation", new StringContent(requestContent, Encoding.UTF8, "application/json"));
            var response = postRequest.Result;

            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);

            var newCompensation = response.DeserializeContent<Compensation>();
            Assert.IsNotNull(newCompensation);
            Assert.IsNotNull(newCompensation.Employee);

            //would be easier if we could instantiate an employee with direct reports in a simpler fashion
            Assert.AreEqual(newCompensation.Employee.EmployeeId, employeeId);
            Assert.AreEqual(newCompensation.Employee.FirstName, fName);
            Assert.AreEqual(newCompensation.Employee.LastName, lName);
            Assert.AreEqual(newCompensation.Employee.Position, position);
            Assert.AreEqual(newCompensation.Employee.Department, department);
        }

        [TestMethod]
        public static void CreateCompensation_Returns_NotFound()
        {
            //not likely to happen but need to make sure its handled regardless
            Compensation compensation = new Compensation()
            {
                CompensationId = "",
                Employee = null,
                Salary = 123456.78,
                EffectiveDate = DateTime.Now,
            };

            var requestContent = new JsonSerialization().ToJson(compensation);
            var postRequest = _client.PostAsync("api/compensation", new StringContent(requestContent, Encoding.UTF8, "application/json"));
            var response = postRequest.Result;

            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [TestMethod]
        public static void GetCompensationById_Returns_Ok()
        {
            var employeeId = "16a596ae-edd3-4847-99fe-c4518e82c86f";
            var fName = "John";
            var lName = "Lennon";
            var position = "Development Manager";
            var department = "Engineering";
            //Add compensation
            var compensation = new CompensationDto()
            {
                EmployeeId = employeeId,
                Salary = 123456.78,
                EffectiveDate = DateTime.Now,
            };

            //normally would be better to create a temp DB with compensations in it and get from there
            var postRequestContent = new JsonSerialization().ToJson(compensation);
            var postRequest = _client.PostAsync("api/compensation", new StringContent(postRequestContent, Encoding.UTF8, "application/json"));

            var getRequestContent = _client.GetAsync($"api/compensation/{compensation.EmployeeId}");
            var response = getRequestContent.Result;

            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);

            var newCompensation = response.DeserializeContent<Compensation>();
            Assert.AreEqual(compensation, newCompensation);

            Assert.AreEqual(newCompensation.Employee.EmployeeId, employeeId);
            Assert.AreEqual(newCompensation.Employee.FirstName, fName);
            Assert.AreEqual(newCompensation.Employee.LastName, lName);
            Assert.AreEqual(newCompensation.Employee.Position, position);
            Assert.AreEqual(newCompensation.Employee.Department, department);
        }

        public static void GetCompensationById_Returns_NotFound()
        {
            var EmployeeId = "Invalid_Id";

            var getRequestContent = _client.GetAsync($"api/compensation/{EmployeeId}");
            var response = getRequestContent.Result;

            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
