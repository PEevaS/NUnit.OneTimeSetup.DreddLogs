using System.Xml;
using FluentAssertions;
using FluentAssertions.Execution;
using NUnit.Engine;
using NUnit.Framework;
using NUnit.OneTimeSetup.DreddLogs.Attributes;
using NUnit.OneTimeSetup.DreddLogs.Exceptions;
using NUnit.OneTimeSetup.DreddLogs.Tests.Internal;

namespace NUnit.OneTimeSetup.DreddLogs.Tests
{
    [TestFixture]
    public class NUnitTestResultTests
    {
        private ITestRunner _testRunner;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            var testPackage = new TestPackage($"NUnit.OneTimeSetup.DreddLogs.Tests.Internal.dll");
            _testRunner = TestEngineActivator.CreateInstance().GetRunner(testPackage);
        }

        [Test]
        public void ExceptionsThrownInFixtureSetup_ShouldBeWrappedByFixtureSetupException()
        {
            var testFilterXml = $@"<filter><not><class>{typeof(IgnoreInOneTimeSetupTests).FullName}</class></not></filter>";
            var filter = new TestFilter(testFilterXml);

            var testCaseCount = _testRunner.CountTestCases(filter);
            testCaseCount.Should().BeGreaterThan(0);

            var xmlReport = _testRunner.Run(null, filter);
            var testCaseNodes = xmlReport.SelectNodes("//test-case");

            using (new AssertionScope())
            {
                testCaseNodes.Count.Should().Be(testCaseCount);

                var fixtureSetupExceptionFullName = typeof(FixtureSetupException).FullName;
                var dreddLoggingAttributeFullName = typeof(DreddLoggingAttribute).FullName;

                foreach (var testCaseNode in testCaseNodes)
                {
                    var xmlNode = testCaseNode as XmlNode;

                    var isFailed = xmlNode.SelectSingleNode("./failure") is not null;
                    isFailed.Should().BeTrue();

                    var exName = nameof(FixtureSetupException);

                    var message = xmlNode.SelectSingleNode("./failure/message").InnerText;
                    message.Should().Contain($"{fixtureSetupExceptionFullName} : Exception was thrown in fixture setup")
                                    .And.Contain("Previous logs")
                                    .And.Contain("----> System.Exception")
                                    .And.NotContain($"{dreddLoggingAttributeFullName}");
                }
            }
        }

        [Test]
        public void NunitResultStateExceptionsShouldNotBeCaught()
        {
            var testFilterXml = $@"<filter><class>{typeof(IgnoreInOneTimeSetupTests).FullName}</class></filter>";
            var filter = new TestFilter(testFilterXml);

            var xmlReport = _testRunner.Run(null, filter);
            var testCaseNodes = xmlReport.SelectNodes("//test-case");

            using (new AssertionScope())
            {
                foreach (var testCaseNode in testCaseNodes)
                {
                    var xmlNode = testCaseNode as XmlNode;
                    var testCaseResult = xmlNode.Attributes["result"];
                    testCaseResult.Value.Should().Be("Skipped");
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
