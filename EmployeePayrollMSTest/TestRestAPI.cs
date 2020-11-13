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
        /// UC4
        /// Tests the update data using put operation.
        /// </summary>
        [TestMethod]
        public void TestUpdateDataUsingPutOperation()
        {
            //making a request for a particular employee to be updated
            RestRequest request = new RestRequest("employees/8", Method.PUT);
            //creating a jobject for new data to be added in place of old
            //json represents a new json object
            JObject jobject = new JObject();
            jobject.Add("name", "Tom Cruise");
            jobject.Add("salary", 5550000);
            //adding parameters in request
            //request body parameter type signifies values added using add.
            request.AddParameter("application/json", jobject, ParameterType.RequestBody);
            //executing request using client
            //IRest response act as a container for the data sent back from api.
            IRestResponse response = client.Execute(request);
            //checking status code of response
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
            //deserializing content added in json file
            Employee dataResponse = JsonConvert.DeserializeObject<Employee>(response.Content);
            //asserting for salary
            Assert.AreEqual(dataResponse.salary, "5550000");
        }
    }
}
