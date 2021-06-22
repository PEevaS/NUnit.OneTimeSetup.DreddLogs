using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AspectInjector.Broker;
using NUnit.Framework;
using NUnit.OneTimeSetup.DreddLogs.Exceptions;

namespace NUnit.OneTimeSetup.DreddLogs.Attributes
{
    [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class)]
    [Aspect(Scope.Global)]
    [Injection(typeof(DreddLoggingAttribute))]
    public sealed class DreddLoggingAttribute : Attribute
    {
        [Advice(Kind.Around, Targets = Target.Method | Target.Public)]
        public object Handle(
            [Argument(Source.Target)] Func<object[], object> target,
            [Argument(Source.Arguments)] object[] args,
            [Argument(Source.Name)] string methodName,
            [Argument(Source.ReturnType)] Type returnType)
        {

            if (IsFixtureSetupMethod(target.Method.ReflectedType, methodName, args) is false)
            {
                return target(args);
            }

            if (IsAsyncMethod(returnType))
            {
                return WrapAsync(target, args).GetAwaiter().GetResult();
            }

            return Wrap(target, args);
        }

        private bool IsFixtureSetupMethod(Type reflectedType, string methodName, object[] parameters)
        {
            if (parameters.Count() == 0)
            {
                return reflectedType.GetMethod(methodName, Array.Empty<Type>()).GetCustomAttribute<OneTimeSetUpAttribute>() != null;
            }

            return false;
        }

        private bool IsAsyncMethod(Type returnType)
        {
            return typeof(Task).IsAssignableFrom(returnType);
        }

        private object Wrap(Func<object[], object> target, object[] args)
        {
            try
            {
                return target(args);
            }
            catch (Exception e)
            {
                throw new FixtureSetupException(e);
            }
        }

        private async Task<object> WrapAsync(Func<object[], object> target, object[] args)
        {
            try
            {
                await (Task)target(args);
                return Task.CompletedTask;
            }
            catch (Exception e)
            {
                throw new FixtureSetupException(e);
            }
        }
    }
}
