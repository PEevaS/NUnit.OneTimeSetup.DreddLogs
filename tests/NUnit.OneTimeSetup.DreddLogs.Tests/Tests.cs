using System.Xml;
using FluentAssertions;
using FluentAssertions.Execution;
using NUnit.Engine;
using NUnit.Framework;

namespace NUnit.OneTimeSetup.DreddLogs.Tests
{
    [TestFixture]
    public class Tests
    {
        private ITestRunner _testRunner;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            var testPackage = new TestPackage($"NUnit.OneTimeSetup.DreddLogs.Tests.Internal.dll");
            _testRunner = TestEngineActivator.CreateInstance().GetRunner(testPackage);
        }

        [Test]
        public void SimpleTest()
        {
            var testCaseCount = _testRunner.CountTestCases(TestFilter.Empty);
            testCaseCount.Should().BeGreaterThan(0);

            var xmlReport = _testRunner.Run(null, TestFilter.Empty);
            var testCaseNodes = xmlReport.SelectNodes("//test-case");

            using (new AssertionScope())
            {
                testCaseNodes.Count.Should().Be(testCaseCount);

                foreach (var testCaseNode in testCaseNodes)
                {
                    var xmlNode = testCaseNode as XmlNode;

                    var isFailed = xmlNode.SelectSingleNode("./failure") is not null;
                    isFailed.Should().BeTrue();

                    var message = xmlNode.SelectSingleNode("./failure/message").InnerText;
                    message.Should().Contain("NUnit.OneTimeSetup.DreddLogs.Exceptions.FixtureSetupException : Exception was thrown in fixture setup")
                                    .And.Contain("Previous logs")
                                    .And.Contain("----> System.Exception");
                }
            }
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            _testRunner.Dispose();
        }
    }
}
