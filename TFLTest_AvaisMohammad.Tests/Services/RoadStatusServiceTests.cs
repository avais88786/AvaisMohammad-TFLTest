using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;
using RichardSzalay.MockHttp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using TFLTest_AvaisMohammad.Models;
using TFLTest_AvaisMohammad.Services;

namespace TFLTest_AvaisMohammad.Tests.Services
{
    [TestClass]
    public class RoadStatusServiceTests
    {
        private RoadStatusService sut;

        private Mock<IConfiguration> mockedConfiguration;

        private HttpClient httpClient;

        private MockHttpMessageHandler mockHttpMessageHandler;

        public RoadStatusServiceTests()
        {
            mockedConfiguration = new Mock<IConfiguration>();
            mockHttpMessageHandler = new MockHttpMessageHandler();
            httpClient = new HttpClient(mockHttpMessageHandler);
            sut = new RoadStatusService(mockedConfiguration.Object, httpClient);

        }

        [TestMethod]
        public void GetStatus_SuccessStatus_ValidResponse()
        {
            //Arrange

            var roadStatuses = new List<RoadStatus>() { new RoadStatus() { DisplayName = "Good", Id = "1", StatusSeverity = "Closed" } };
            mockedConfiguration.SetupGet(m => m["apiEndpointBase"]).Returns("http://myBase");
            mockedConfiguration.SetupGet(m => m["appId"]).Returns("AppId");
            mockedConfiguration.SetupGet(m => m["appKey"]).Returns("AppKey");

            mockHttpMessageHandler.When("http://myBase/*")
                .Respond("application/json", JsonConvert.SerializeObject(roadStatuses)); // Respond with JSON

            //Act
            var result = sut.GetStatus("A32");

            //Assert
            Assert.AreEqual(roadStatuses.First().Id, result.Result.Id);
        }

        [TestMethod]
        public void GetStatus_SuccessStatus_InValidResponse()
        {
            //Arrange

            mockedConfiguration.SetupGet(m => m["apiEndpointBase"]).Returns("http://myBase");
            mockedConfiguration.SetupGet(m => m["appId"]).Returns("AppId");
            mockedConfiguration.SetupGet(m => m["appKey"]).Returns("AppKey");

            mockHttpMessageHandler.When("http://myBase/*")
                .Respond("application/json", "{'name' : 'Foo Bar'}");

            //Act
            Assert.ThrowsException<AggregateException>(() => sut.GetStatus("A32").Result);

        }

        [TestMethod]
        public void GetStatus_NotFound()
        {
            //Arrange
            mockedConfiguration.SetupGet(m => m["apiEndpointBase"]).Returns("http://myBase");
            mockedConfiguration.SetupGet(m => m["appId"]).Returns("AppId");
            mockedConfiguration.SetupGet(m => m["appKey"]).Returns("AppKey");

            HttpResponseMessage httpResponseMessage = new HttpResponseMessage(System.Net.HttpStatusCode.NotFound)
            {
                Content = new StringContent(JsonConvert.SerializeObject(new Exception("hello")))
            };

            mockHttpMessageHandler.When("http://myBase/*").Respond(g => httpResponseMessage);

            //Act
            var x = sut.GetStatus("A32").Result;

            //Assert
            Assert.IsFalse(x.Found);
        }

        [TestMethod]
        public void GetStatus_NetworkError()
        {
            //Arrange

            mockedConfiguration.SetupGet(m => m["apiEndpointBase"]).Returns("http://myBase");
            mockedConfiguration.SetupGet(m => m["appId"]).Returns("AppId");
            mockedConfiguration.SetupGet(m => m["appKey"]).Returns("AppKey");

            HttpResponseMessage httpResponseMessage = new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
            mockHttpMessageHandler.When("http://myBase/*").Respond(g => httpResponseMessage);

            //Act
            var x = sut.GetStatus("A32").Result;

            //Assert
            Assert.IsFalse(x.Found);
        }

    }
}
