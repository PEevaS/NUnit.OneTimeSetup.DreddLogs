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
        [TestCase(null)]
        public void CallOverloadedMethodWithParameters(string str)
        {
            OverloadedMethod(str);
        }

        [Test]
        public void CallOverloadedMethodWithoutParameters()
        {
            OverloadedMethod();
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
