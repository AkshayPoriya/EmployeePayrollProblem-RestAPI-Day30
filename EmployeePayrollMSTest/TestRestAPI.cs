using EmployeePayrollSystem;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;

namespace EmployeePayrollMSTest
{
    [TestClass]
    public class TestRestAPI
    {
        //declaring restclient variable
        RestClient client;
        /// <summary>
        /// Setups this instance for the client by giving url along with port.
        /// </summary>
        [TestInitialize]
        public void Setup()
        {
            client = new RestClient("http://localhost:4000");
        }
        /// <summary>
        /// Gets the employee list in the form of irestresponse. 
        /// </summary>
        /// <returns>IRestResponse response</returns>
        private IRestResponse getEmployeeList()
        {
            //arrange
            //makes restrequest for getting all the data from json server by giving table name and method.get
            RestRequest request = new RestRequest("/employees", Method.GET);

            //act
            //executing the request using client and saving the result in IrestResponse.
            IRestResponse response = client.Execute(request);
            return response;
        }
        /// <summary>
        /// Ons the calling get API return employee list.
        /// </summary>
        [TestMethod]
        public void onCallingGetApi_ReturnEmployeeList()
        {
            //gets the irest response from getemployee list method
            IRestResponse response = getEmployeeList();
            //assert
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
            //adding the data into list from irestresponse by using deserializing.
            List<Employee> dataResponse = JsonConvert.DeserializeObject<List<Employee>>(response.Content);
            Assert.AreEqual(5, dataResponse.Count);
            foreach (Employee employee in dataResponse)
            {
                Console.WriteLine("Id: " + employee.id + " Name: " + employee.name + " Salary: " + employee.salary);
            }            
        }
    }
}
