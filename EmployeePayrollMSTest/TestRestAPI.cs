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
        /// UC5
        /// Tests the delete operation.
        /// </summary>
        [TestMethod]
        public void TestDeleteDataUsingDeleteOperation()
        {
            //Arrange
            RestRequest request = new RestRequest("employees/8", Method.DELETE);
            //Act
            IRestResponse response = client.Execute(request);
            //Assert
            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.OK);
        }
    }
}
