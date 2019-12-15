using System;
using System.IO;
using System.Reflection;
using DataContracts;
using MaximumPath;
using MaximumPath.Config;
using MaximumPath.DataAccess;
using MaximumPath.PathFinders;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TechTalk.SpecFlow;

namespace IntegrationTests
{
    [Binding]
    public class MaximumPathSteps
    {
        private InputProviderFromFile InputProvider { get; set; }
        private SmartMaximumPathFinder PathFinder { get; set; }
        private Result Result { get; set; }


        [Given(@"pyramid is defined in '(.*)'")]
        public void GivenPyramidIsDefinedInFile(string filename)
        {
            var currentPath = Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath);

            var config = new InputFileConfig
            {
                Filename = $"{currentPath}/Input/{filename}"
            };

            InputProvider = new InputProviderFromFile(config);
        }

        [Given(@"smart maximum path finder algorithm is used")]
        public void GivenSmartMaximumPathFinderIsUsed()
        {
            PathFinder = new SmartMaximumPathFinder();
        }

        [When(@"algorithm  is executed")]
        public void WhenAlgorithmIsExecuted()
        {
            var validatorMock = new Mock<IPyramidValidator>();
            validatorMock.Setup(x => x.IsValid(It.IsAny<Pyramid>())).Returns(true);

            Result = Program.Process(InputProvider, validatorMock.Object, PathFinder);
        }

        [Then(@"the output is equal to '(.*)' content")]
        public void ThenTheOutputIsEqualToFileContent(string fileName)
        {
            var outputWriter = new TestOutputWriter();
            Program.WriteOutput(outputWriter, Result);

            var currentPath = Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath);
            var expectedResponse = File.ReadAllText($"{currentPath}/ExpectedOutcome/{fileName}");

            Assert.AreEqual(expectedResponse, outputWriter.OutputBuilder.ToString());
        }
    }
}
