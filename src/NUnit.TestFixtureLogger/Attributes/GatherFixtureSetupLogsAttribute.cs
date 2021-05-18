using System;
using System.Reflection;
using System.Threading.Tasks;
using AspectInjector.Broker;
using NUnit.Framework;
using NUnit.TestFixtureLogger.Exceptions;

namespace NUnit.TestFixtureLogger.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    [Aspect(Scope.Global)]
    [Injection(typeof(GatherFixtureSetupLogsAttribute))]
    public class GatherFixtureSetupLogsAttribute : Attribute
    {
        [Advice(Kind.Around, Targets = Target.Method | Target.Public)]
        public object Handle(
            [Argument(Source.Target)] Func<object[], object> target,
            [Argument(Source.Arguments)] object[] args,
            [Argument(Source.Name)] string methodName,
            [Argument(Source.ReturnType)] Type returnType)
        {

            var isFixtureSetupMethod = IsFixtureSetupMethod(target.Method.ReflectedType, methodName);

            if (IsAsyncMethod(returnType))
            {
                return WrapAsync(target, args, isFixtureSetupMethod).GetAwaiter().GetResult();
            }

            return Wrap(target, args, isFixtureSetupMethod);
        }

        private bool IsFixtureSetupMethod(Type reflectedType, string methodName)
        {
            return reflectedType.GetMethod(methodName).GetCustomAttribute<OneTimeSetUpAttribute>() != null;
        }

        private bool IsAsyncMethod(Type returnType)
        {
            return typeof(Task).IsAssignableFrom(returnType);
        }

        private object Wrap(Func<object[], object> target, object[] args, bool fixtureSetupMethod)
        {
            try
            {
                return target.Invoke(args);
            }
            catch (Exception e)
            {
                if (fixtureSetupMethod)
                {
                    throw new FixtureSetupException(e);
                }

                throw;
            }
        }

        private async Task<object> WrapAsync(Func<object[], object> target, object[] args, bool fixtureSetupMethod)
        {
            try
            {
                await (Task)target.Invoke(args);
                return new object();
            }
            catch (Exception e)
            {
                if (fixtureSetupMethod)
                {
                    throw new FixtureSetupException(e);
                }

                throw;
            }
        }
    }
}
