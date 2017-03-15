using DevOpsFlex.Core;
using DevOpsFlex.Tests.Core;
using System;
using System.Threading;
using FluentAssertions;
using Xunit;

// ReSharper disable once CheckNamespace
public class LittleBrotherPushConnectorTest
{
    [Fact, IsUnit]
    public void Test_Connect()
    {
        var resetEvent = new ManualResetEvent(false);
        var lb = new LittleBrother();
        var @event = new TestEvent();
        new TestLittleBrotherConnector().Connect().Subscribe(
            e =>
            {
                e.Should().Be(@event);
                resetEvent.Set();
            });

        lb.Publish(@event);

        resetEvent.WaitOne();
    }
}

public class TestLittleBrotherConnector : LittleBrotherPushConnector
{
}

public class TestEvent : BbTelemetryEvent
{
}