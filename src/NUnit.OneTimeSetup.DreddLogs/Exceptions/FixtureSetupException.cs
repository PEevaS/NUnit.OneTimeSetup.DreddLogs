using System;
using System.Text;
using System.Text.RegularExpressions;
using NUnit.Framework.Internal;
using NUnit.OneTimeSetup.DreddLogs.Attributes;

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
                  .AppendLine($"{InnerException.GetType().FullName}: {InnerException.Message}")
                  .AppendLine(GetPurifiedInnerExceptionStacktrace())
                  .AppendLine("Previous logs:")
                  .Append(TestExecutionContext.CurrentContext.CurrentResult.Output);

                return sb.ToString();
            }
        }

        private string GetPurifiedInnerExceptionStacktrace()
        {
            var sb = new StringBuilder();

            sb.AppendLine(PurifyStacktrace(StackTrace))
              .Append(PurifyStacktrace(InnerException.StackTrace));

            return sb.ToString();
        }

        private string PurifyStacktrace(string stackTrace)
        {
            var sb = new StringBuilder();

            var lines = stackTrace.Split("\r\n");
            foreach (var line in lines)
            {
                if (!string.IsNullOrEmpty(line) && !line.Contains("$_around") && !line.Contains(typeof(DreddLoggingAttribute).FullName))
                {
                    sb.AppendLine(line);
                }
            }

            return sb.ToString();
        }
    }
}
