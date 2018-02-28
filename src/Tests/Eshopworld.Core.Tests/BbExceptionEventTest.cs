using System;
using System.Diagnostics.CodeAnalysis;
using Eshopworld.Core;
using Eshopworld.Tests.Core;
using FluentAssertions;
using Newtonsoft.Json;
using Xunit;

// ReSharper disable once CheckNamespace
[ExcludeFromCodeCoverage]
public class BbExceptionEventTest
{
    [Fact, IsUnit]
    public void Ensure_ExceptionIsntSerialized()
    {
        var json = JsonConvert.SerializeObject(new BbExceptionEvent(new Exception()));
        var poco = JsonConvert.DeserializeObject<BbExceptionEvent>(json);

        poco.Exception.Should().BeNull();
    }

    public class ToBbEvent
    {
        [Fact, IsUnit]
        public void Test_BbEventPopulate()
        {
            const string exceptionMessage = "KABUM!!!";
            var exception = new Exception(exceptionMessage);

            var bbEvent = exception.ToBbEvent();

            bbEvent.Exception.Message.Should().Be(exceptionMessage);
        }
    }
}
