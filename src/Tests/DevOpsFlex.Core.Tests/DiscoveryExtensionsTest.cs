using System;
using DevOpsFlex.Core;
using DevOpsFlex.Tests.Core;
using FluentAssertions;
using Xunit;

// ReSharper disable once CheckNamespace
public class DiscoveryExtensionsTest
{
    public class GetConnectors
    {
        [Fact, IsUnit]
        public void Test_GetSinglePull()
        {
            var result = AppDomain.CurrentDomain.GetPullConnectors();
            result.Should().ContainSingle(c => c.GetType() == typeof(TestPullConnector));
        }

        [Fact, IsUnit]
        public void Test_GetSinglePush()
        {
            var result = AppDomain.CurrentDomain.GetPushConnectors();
            result.Should().ContainSingle(c => c.GetType() == typeof(TestPushConnector));
        }
    }
}

public class TestPullConnector : IPullTelemetry
{
    public void Connect(IObserver<BbEvent> stream)
    {
        throw new NotImplementedException();
    }
}

public class TestPushConnector : IPushTelemetry
{
    public IObservable<BbEvent> Connect()
    {
        throw new NotImplementedException();
    }
}
