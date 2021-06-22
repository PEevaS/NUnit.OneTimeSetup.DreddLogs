using NUnit.Framework;
using NUnit.OneTimeSetup.DreddLogs.Attributes;
using NUnit.OneTimeSetup.DreddLogs.Tests.Internal;

namespace NUnit.OneTimeSetup.DreddLogs.Tests
{
    [DreddLogging]
    [TestFixture]
    public class OverloadedMethodsTests : BaseTest
    {
        [TestCase("hello")]
        [TestCase("world")]
        public void ParameterizedTest(string str)
        {
            OverloadedMethod(str);
        }

        public void OverloadedMethod()
        {
            System.Console.WriteLine("hello world");
        }

        public void OverloadedMethod(string str)
        {
            System.Console.WriteLine(str);
        }
    }
}
