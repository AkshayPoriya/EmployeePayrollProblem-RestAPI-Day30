using EmployeePayrollSystem;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;

namespace EmployeePayrollMSTest
{
    [TestClass]
    public class TestRestAPI
    {
        RestClient client;
        
        [TestInitialize]
        public void Setup()
        {
            client = new RestClient("http://localhost:4000");
        }

        /// <summary>
        /// UC3
        /// Tests the add multiple entries using post operation.
        /// </summary>
        [TestMethod]
        public void TestAddMultipleEntriesUsingPostOperation()
        {
            //adding multiple employees to table
            List<Employee> employeeList = new List<Employee>();
            employeeList.Add(new Employee { name = "Virat Kohli", salary = "400000" });
            employeeList.Add(new Employee { name = "MSD", salary = "500000" });
            foreach(Employee employee in employeeList)
            {
                RestRequest request = new RestRequest("/employees", Method.POST);
                JObject jObject = new JObject();
                jObject.Add("name", employee.name);
                jObject.Add("salary", employee.salary);
                request.AddParameter("application/json", jObject, ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                //Assert
                Assert.AreEqual(response.StatusCode, HttpStatusCode.Created);
                //derserializing object for assert and checking test case
                Employee dataResponse = JsonConvert.DeserializeObject<Employee>(response.Content);
                Assert.AreEqual(employee.name, dataResponse.name);
            }
        }
    }
}
