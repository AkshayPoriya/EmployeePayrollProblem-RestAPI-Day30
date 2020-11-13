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
        /// UC2
        /// Tests the add data by post operation.
        /// </summary>
        [TestMethod]
        public void TestAddDataByPostOperation()
        {
            //Arrange
            //adding request to post(add) data
            RestRequest request = new RestRequest("/employees", Method.POST);
            //instatiating jObject for adding data for name and salary, id auto increments
            JObject jObject = new JObject();
            jObject.Add("name", "Rohit Sharma");
            jObject.Add("salary", "150000");
            //as parameters are passed as body hence "request body" call is made, in parameter type
            request.AddParameter("application/json", jObject, ParameterType.RequestBody);
            //Act
            //request contains method of post and along with added parameter which contains data to be added
            //hence response will contain the data which is added and not all the data from jsonserver.
            //data is added to json server json file in this step.
            IRestResponse response = client.Execute(request);
            //Assert
            Assert.AreEqual(response.StatusCode, HttpStatusCode.Created);
            //derserializing object for assert and checking test case
            Employee dataResponse = JsonConvert.DeserializeObject<Employee>(response.Content);
            Assert.AreEqual("Rohit Sharma", dataResponse.name);
            Assert.AreEqual("150000", dataResponse.salary);
            Console.WriteLine(response.Content);
        }
    }
}
