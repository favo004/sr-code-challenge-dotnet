using challenge.Models;
using code_challenge.Tests.Integration.Extensions;
using code_challenge.Tests.Integration.Helpers;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net;
using System.Net.Http;
using System.Text;

namespace code_challenge.Tests.Integration
{
    [TestClass]
    public class CompensationControllerTests
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
        public void CreateCompensation_Returns_Created()
        {

            // Initialize dummy object
            var compensation = new Compensation()
            {
                EmployeeId = "03aa1462-ffa9-4978-901b-7c001562cf6f",
                Salary = 15000,
                EffectiveDate = new DateTime(2021, 2, 18)
            };

            var body = new JsonSerialization().ToJson(compensation);

            // Request data from api
            var postRequestTask = _httpClient.PostAsync($"api/compensation",
                new StringContent(body, Encoding.UTF8, "application/json"));
            var response = postRequestTask.Result;

            // Did the response return a 201 created code
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);

            var newCompensation = response.DeserializeContent<Compensation>();
            Assert.IsNotNull(newCompensation);
            Assert.AreEqual("03aa1462-ffa9-4978-901b-7c001562cf6f", newCompensation.EmployeeId);
            Assert.AreEqual(15000, newCompensation.Salary);
            Assert.AreEqual(new DateTime(2021, 2, 18), newCompensation.EffectiveDate);
        }
    }
}
