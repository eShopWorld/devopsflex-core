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
            var result = new[] { GetType().Assembly }.GetConnectors<IPullTelemetry>();
            result.Should().ContainSingle(c => c.GetType() == typeof(TestPullConnector));
        }

        [Fact, IsUnit]
        public void Test_GetSinglePush()
        {
            var result = new[] {GetType().Assembly}.GetConnectors<IPushTelemetry>();
            result.Should().ContainSingle(c => c.GetType() == typeof(TestPushConnector));
        }
    }
}

public class TestPullConnector : IPullTelemetry
{
    public void Connect(IObservable<BbEvent> stream)
    {
        throw new NotImplementedException();
    }
}

public class TestPushConnector : IPushTelemetry
{
    public IObserver<BbEvent> Connect()
    {
        throw new NotImplementedException();
    }
}
