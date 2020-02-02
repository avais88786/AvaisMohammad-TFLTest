using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;
using TFLTest_AvaisMohammad.Models;
using TFLTest_AvaisMohammad.Services;

namespace TFLTest_AvaisMohammad.Specs.StepDefinitions
{
    [Binding]
    public sealed class RoadStatusTestsDefinition
    {
        private readonly RoadStatusService roadStatusService;
        private string _roadName;
        private RoadStatus roadStatus;

        public RoadStatusTestsDefinition()
        {
            IConfigurationRoot configuration = Program.GetConfigurationSettings(Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location.Substring(0, System.Reflection.Assembly.GetEntryAssembly().Location.IndexOf("bin\\"))));

            if (string.IsNullOrEmpty(configuration["apiEndpointBase"]) || string.IsNullOrEmpty(configuration["appId"]) || string.IsNullOrEmpty(configuration["appKey"]))
            {
                throw new Exception($"Configuration settings are invalid. Please configure appsettings.<environment>.json in project folder TFLTest_AvaisMohammad appropriately");
            }

            roadStatusService = new RoadStatusService(configuration, new System.Net.Http.HttpClient());
        }

        [Given(@"a valid road ID (.*) is specified")]
        public void GivenAValidRoadIDAIsSpecified(string roadName)
        {
            _roadName = roadName;
        }

        [When(@"the client is run")]
        public void WhenTheClientIsRun()
        {
            roadStatus = (RoadStatus)roadStatusService.GetStatus(_roadName).Result;
        }

        [Then(@"the following status of the given road should be present")]
        public void ThenTheFollowingStatusOfTheGivenRoadShouldBePresent(Table table)
        {
            foreach(var row in table.Rows)
            {
                Assert.AreEqual(roadStatus.GetType().GetProperty(row[0]).GetValue(roadStatus).ToString(), row[1].ToString());
            }
        }

        [Then(@"Road status found value is (.*)")]
        public void ThenRoadStatusFoundValueIs(bool value)
        {
            Assert.AreEqual(value, roadStatus.Found);
        }



    }
}
