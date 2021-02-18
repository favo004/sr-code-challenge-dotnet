using challenge.Models;
using code_challenge.Tests.Integration.Extensions;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.Net.Http;

namespace code_challenge.Tests.Integration
{
    [TestClass]
    public class ReportingStructureControllerTests
    {

        private static HttpClient _httpClient;
        private static TestServer _testServer;

        [ClassInitialize]
        public static void InitializeClass(TestContext context)
        {
            _testServer = new TestServer(WebHost.CreateDefaultBuilder()
                .UseStartup<TestServerStartup>()
                .UseEnvironment("Development"));

            _httpClient = _testServer.CreateClient();
        }

        [ClassCleanup]
        public static void CleanUpTest()
        {
            _httpClient.Dispose();
            _testServer.Dispose();
        }

        [TestMethod]
        public void GetReportingStructureByEmployeeId_Returns_Ok()
        {
            string employeeId = "16a596ae-edd3-4847-99fe-c4518e82c86f";

            // Request data from api
            var getRequestTask = _httpClient.GetAsync($"api/reporting/{employeeId}");
            var response = getRequestTask.Result;

            // Did the response return a 200 code
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            var reportingStructure = response.DeserializeContent<ReportingStructure>();
            Assert.AreEqual(4, reportingStructure.NumberOfReports);
        }
    }
}
