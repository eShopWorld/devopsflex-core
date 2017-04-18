using System;
using System.Fakes;
using DevOpsFlex.Core;
using DevOpsFlex.Tests.Core;
using FluentAssertions;
using Microsoft.QualityTools.Testing.Fakes;
using Xunit;

// ReSharper disable once CheckNamespace
public class BbTimeEventTest
{
    [Fact, IsUnit]
    public void Test_StartTime()
    {
        using (ShimsContext.Create())
        {
            var now = DateTime.Now; // freeze time
            ShimDateTime.NowGet = () => now;

            var tEvent = new BbTimedEvent();

            tEvent.StartTime.Should().Be(now);
        }
    }

    [Fact, IsUnit]
    public void Test_EndTime()
    {
        using (ShimsContext.Create())
        {
            var now = DateTime.Now; // freeze time
            ShimDateTime.NowGet = () => now;

            var tEvent = new BbTimedEvent();
            tEvent.End();

            tEvent.EndTime.Should().Be(now);
        }
    }
}
