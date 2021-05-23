using System;
using System.Text;
using NUnit.Framework.Internal;

namespace NUnit.OneTimeSetup.DreddLogs.Exceptions
{
    public class FixtureSetupException : Exception
    {
        public FixtureSetupException(Exception e) : base("Exception was thrown in fixture setup", e)
        {
        }

        public override string Message
        {
            get
            {
                var sb = new StringBuilder();

                sb.AppendLine(base.Message)
                  .AppendLine(InnerException.ToString())
                  .AppendLine()
                  .AppendLine("Previous logs:")
                  .AppendLine(TestExecutionContext.CurrentContext.CurrentResult.Output);

                return sb.ToString();
            }
        }
    }
}
