# NUnit.OneTimeSetup.DreddLogs

Helper library for NUnit that allows you to show all the logs that happened during test fixture setup (OneTimeSetup). Uses [aspect-injector](https://github.com/pamidur/aspect-injector)

With NUnit, when fixture setup fails and the exception is thrown, you will get something like this:

```
OneTimeSetUp: System.Exception : <exception's message>
```

And that's all. No stacktrace and any other useful information.
If fixture setup fails during run on CI, there will be no logs displayed.
This happens because fixture setup is not considered as a part of test.
All this makes investigating test failures really inconvenient.

With the use of library, if an exception is thrown during fixture setup, in test result you will get
exception's stacktrace and all the preceding logs. E.g.:

```
OneTimeSetUp: NUnit.OneTimeSetup.DreddLogs.Exceptions.FixtureSetupException : Exception was thrown in fixture setup
    System.Exception: <original exception's message>
       at NUnit.OneTimeSetup.DreddLogs.Tests.TestFixtureWithSynchronousStaticSetupMethod.__a$_around_OneTimeSetup_100663307_o() in D:\Workspace\nunit-fixture-logger\tests\NUnit.OneTimeSetup.DreddLogs.Tests\TestFixtureWithSynchronousNonStaticSetupMethod.cs:line 29
       at NUnit.OneTimeSetup.DreddLogs.Tests.TestFixtureWithSynchronousStaticSetupMethod.__a$_around_OneTimeSetup_100663307_u(Object[] )
       at NUnit.OneTimeSetup.DreddLogs.Attributes.DreddLoggingAttribute.Wrap(Func`2 target, Object[] args) in D:\Workspace\nunit-fixture-logger\src\NUnit.OneTimeSetup.DreddLogs\Attributes\DreddLoggingAttribute.cs:line 50

    Previous logs:
    [21:32:02 INF] OneTimeSetup of TestFixtureWithSynchronousStaticSetupMethod


      ----> System.Exception : Synchronous static fixture setup method
```

## How to use

Just add the **DreddLogging** atribute to your test fixture class or specify it in AssemblyInfo.cs
as assembly-level attribute.
That's it.

## Limitations and known issues

- DreddLogging attribute does not work with inheritance. If you have some base fixture class and all other fixture classes are inherited from it, and you specify [DreddLogging] atribute in base class then it's functionality won't be inherited in derived classes. Known issue of aspect-injector - [#101](https://github.com/pamidur/aspect-injector/issues/101).
- For all others, refer to [aspect-injector](https://github.com/pamidur/aspect-injector)
