using System;
using DevOpsFlex.Core;
using DevOpsFlex.Tests.Core;
using FluentAssertions;
using Xunit;

// ReSharper disable once CheckNamespace
public class BbTimeEventTest
{
    [Fact, IsUnit]
    public void Test_StartTime()
    {
        DateTime now = DateTime.Now;
        BbTimedEvent.DateTimeNow = () => now;
        var tEvent = new BbTimedEvent();

        tEvent.StartTime.Should().Be(now);
    }

    [Fact, IsUnit]
    public void Test_EndTime()
    {
        var now = DateTime.Now; // freeze time
        BbTimedEvent.DateTimeNow = () => now;

        var tEvent = new BbTimedEvent();
        tEvent.End();

        tEvent.EndTime.Should().Be(now);
    }

    [Fact, IsUnit]
    public void Test_EndTime_IsGuardedForMultipleCalls()
    {
        var now = DateTime.Now; // freeze time
        var nowPlus10 = DateTime.Now.AddMinutes(10); // freeze time 10 minutes later

        BbTimedEvent.DateTimeNow = () => now;

        var tEvent = new BbTimedEvent();
        tEvent.End();

        BbTimedEvent.DateTimeNow = () => nowPlus10;

        tEvent.End();
        tEvent.EndTime.Should().Be(now);
    }

    [Fact, IsUnit]
    public void Test_AfterEnd()
    {
        var now = DateTime.Now; // freeze time
        var nowPlus10 = DateTime.Now.AddMinutes(10); // freeze time 10 minutes later

        BbTimedEvent.DateTimeNow = () => now;

        var tEvent = new BbTimedEvent();
        BbTimedEvent.DateTimeNow = () => nowPlus10;
        tEvent.End();

        tEvent.ProcessingTime.Should().BeCloseTo(TimeSpan.FromMinutes(10), 1000);
    }

    [Fact, IsUnit]
    public void Test_BeforeEnd()
    {
        var now = DateTime.Now; // freeze time
        var nowPlus10 = DateTime.Now.AddMinutes(10); // freeze time 10 minutes later

        BbTimedEvent.DateTimeNow = () => now;

        var tEvent = new BbTimedEvent();

        BbTimedEvent.DateTimeNow = () => nowPlus10;

        tEvent.ProcessingTime.Should().BeCloseTo(TimeSpan.FromMinutes(10), 1000);
    }
}
