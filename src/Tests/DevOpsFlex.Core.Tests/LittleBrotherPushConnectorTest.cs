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
        var received = false;
        var lb = new LittleBrother();
        var testEvent = new TestEvent();

        new TestLittleBrotherConnector().Connect().Subscribe(
            e =>
            {
                e.Should().Be(testEvent);
                resetEvent.Set();
                received = true;
            });

        lb.Publish(testEvent);

        resetEvent.WaitOne(TimeSpan.FromSeconds(5));
        received.Should().BeTrue();
    }
}

public class TestLittleBrotherConnector : LittleBrotherPushConnector
{
}

public class TestEvent : BbTelemetryEvent
{
}