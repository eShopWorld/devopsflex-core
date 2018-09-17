using System;
using Eshopworld.Core;
using Eshopworld.Tests.Core;
using FluentAssertions;
using Newtonsoft.Json;
using Xunit;

// ReSharper disable once CheckNamespace
public class ExceptionEventTest
{
    [Fact, IsUnit]
    public void Ensure_ExceptionIsntSerialized()
    {
        var json = JsonConvert.SerializeObject(new ExceptionEvent(new Exception()));
        var poco = JsonConvert.DeserializeObject<ExceptionEvent>(json);

        poco.Exception.Should().BeNull();
    }

    public class ToBbEvent
    {
        [Fact, IsUnit]
        public void Test_BbEventPopulate()
        {
            const string exceptionMessage = "KABUM!!!";
            var exception = new Exception(exceptionMessage);

            var bbEvent = exception.ToExceptionEvent();

            bbEvent.Exception.Message.Should().Be(exceptionMessage);
        }
    }
}
