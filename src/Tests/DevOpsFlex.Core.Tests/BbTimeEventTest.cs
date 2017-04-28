using System;
using System.Fakes;
using System.Security.Policy;
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

    [Fact, IsUnit]
    public void Test_EndTime_IsGuardedForMultipleCalls()
    {
        using (ShimsContext.Create())
        {
            var now = DateTime.Now; // freeze time
            var nowPlus10 = DateTime.Now.AddMinutes(10); // freeze time 10 minutes later

            ShimDateTime.NowGet = () => now;

            var tEvent = new BbTimedEvent();
            tEvent.End();

            ShimDateTime.NowGet = () => nowPlus10;

            tEvent.End();
            tEvent.EndTime.Should().Be(now);
        }
    }

    public class ProcessingTime
    {
        [Fact, IsUnit]
        public void Test_AfterEnd()
        {
            using (ShimsContext.Create())
            {
                var now = DateTime.Now; // freeze time
                var nowPlus10 = DateTime.Now.AddMinutes(10); // freeze time 10 minutes later

                ShimDateTime.NowGet = () => now;

                var tEvent = new BbTimedEvent();
                ShimDateTime.NowGet = () => nowPlus10;
                tEvent.End();

                tEvent.ProcessingTime.Should().BeCloseTo(TimeSpan.FromMinutes(10), 1000);
            }
        }

        [Fact, IsUnit]
        public void Test_BeforeEnd()
        {
            using (ShimsContext.Create())
            {
                var now = DateTime.Now; // freeze time
                var nowPlus10 = DateTime.Now.AddMinutes(10); // freeze time 10 minutes later

                ShimDateTime.NowGet = () => now;

                var tEvent = new BbTimedEvent();
                ShimDateTime.NowGet = () => nowPlus10;

                tEvent.ProcessingTime.Should().BeCloseTo(TimeSpan.FromMinutes(10), 1000);
            }
        }
    }
}
