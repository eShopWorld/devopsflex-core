using System;
using Eshopworld.Core;
using Eshopworld.Tests.Core;
using FluentAssertions;
using Xunit;

// ReSharper disable once CheckNamespace
public class TimedTelemetryEventTest
{
    private static readonly DateTime Now = new DateTime(2020, 1, 1, 10, 1, 1, DateTimeKind.Utc);

    [Fact, IsUnit]
    public void Test_StartTime()
    {
        TimedTelemetryEvent.GetDateTimeUtcNow = () => Now;
        var tEvent = new TimedTelemetryEvent();

        tEvent.StartTime.Should().Be(Now);
    }

    [Fact, IsUnit]
    public void Test_EndTime()
    {
        TimedTelemetryEvent.GetDateTimeUtcNow = () => Now;

        var tEvent = new TimedTelemetryEvent();
        tEvent.End();

        tEvent.EndTime.Should().Be(Now);
    }

    [Fact, IsUnit]
    public void Test_EndTime_IsGuardedForMultipleCalls()
    {
        var nowPlus10 = Now.AddMinutes(10); // freeze time 10 minutes later

        TimedTelemetryEvent.GetDateTimeUtcNow = () => Now;

        var tEvent = new TimedTelemetryEvent();
        tEvent.End();

        TimedTelemetryEvent.GetDateTimeUtcNow = () => nowPlus10;

        tEvent.End();
        tEvent.EndTime.Should().Be(Now);
    }

    [Fact, IsUnit]
    public void Test_AfterEnd()
    {
        var nowPlus10 = Now.AddMinutes(10); // freeze time 10 minutes later

        TimedTelemetryEvent.GetDateTimeUtcNow = () => Now;

        var tEvent = new TimedTelemetryEvent();
        TimedTelemetryEvent.GetDateTimeUtcNow = () => nowPlus10;
        tEvent.End();

        tEvent.ProcessingTime.Should().Be(TimeSpan.FromMinutes(10));
    }

    [Fact, IsUnit]
    public void Test_BeforeEnd()
    {
        var nowPlus10 = Now.AddMinutes(10); // freeze time 10 minutes later

        TimedTelemetryEvent.GetDateTimeUtcNow = () => Now;

        var tEvent = new TimedTelemetryEvent();

        TimedTelemetryEvent.GetDateTimeUtcNow = () => nowPlus10;

        tEvent.ProcessingTime.Should().Be(TimeSpan.FromMinutes(10));
    }
}
