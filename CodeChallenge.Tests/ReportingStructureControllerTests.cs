using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using CodeCodeChallenge.Tests.Integration.Helpers;
using CodeChallenge.Models;
using CodeCodeChallenge.Tests.Integration.Extensions;

namespace CodeChallenge.Tests.Integration
{
    [TestClass]
    public class ReportingStructureControllerTests
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
        public void GetReportingStructure_Returns_Ok()
        {
            var employeeId = "16a596ae-edd3-4847-99fe-c4518e82c86f";
            //without any changes, John will have 4 reports
            int reportingNumber = 4;

            var getRequest = _client.GetAsync($"api/reportingStructure/{employeeId}");
            var response = getRequest.Result;

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            //a little iffy with this case since it's hard coded, but need to make sure that we
            //get the correct reporting number back and not just the correct response
            ReportingStructure reporting = response.DeserializeContent<ReportingStructure>();
            Assert.AreEqual(reportingNumber, reporting.NumberOfReports);
        }

        [TestMethod]
        public void GetReportingStructure_Returns_NotFound()
        {
            var employeeId = "Invalid_ID";

            var getRequest = _client.GetAsync($"api/reportingStructure/{employeeId}");
            var response = getRequest.Result;

            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
