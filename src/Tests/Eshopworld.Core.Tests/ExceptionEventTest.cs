using System;
using System.Net;
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

        [Fact, IsUnit]
        public void Test_ExceptionCustomProperties()
        {
            var exc = new CustomTestException
                {CustomByte = 123, CustomEnum = HttpStatusCode.Accepted, CustomString = "blah"};

            var bbEvent = exc.ToExceptionEvent();

            var dict = bbEvent.ToStringDictionary();
            dict[nameof(CustomTestException.CustomString)].Should().Be("blah");
            dict[nameof(CustomTestException.CustomByte)].Should().Be(123.ToString());
            dict[nameof(CustomTestException.CustomEnum)].Should()
                .Be(((int) HttpStatusCode.Accepted).ToString());
            dict.ContainsKey(nameof(AnonymousTelemetryEvent.CallerMemberName)).Should().BeTrue();
            dict.ContainsKey(nameof(AnonymousTelemetryEvent.CallerFilePath)).Should().BeTrue();
            dict.ContainsKey(nameof(AnonymousTelemetryEvent.CallerLineNumber)).Should().BeTrue();
        }


        [Fact, IsUnit]
        public void Test_ExceptionCustomProperties_AdjunctObjectThrows()
        {
            var exc = new ExceptionThrowingTestException{ CustomString = "blah" };

            var bbEvent = exc.ToExceptionEvent();

            var dict = bbEvent.ToStringDictionary();
            dict.Should().NotContainKey(nameof(ExceptionThrowingTestException.CustomString));
            dict.ContainsKey(nameof(AnonymousTelemetryEvent.CallerMemberName)).Should().BeTrue();
            dict.ContainsKey(nameof(AnonymousTelemetryEvent.CallerFilePath)).Should().BeTrue();
            dict.ContainsKey(nameof(AnonymousTelemetryEvent.CallerLineNumber)).Should().BeTrue();
        }

        public class ExceptionThrowingTestException : Exception
        {
            public string CustomString
            {
                get => throw new Exception();
                // ReSharper disable once ValueParameterNotUsed
                set
                {
                }
            }
        }

        private class CustomTestException : Exception
        {
            public string? CustomString { get; set; }
            public byte CustomByte { get; set; }
            public HttpStatusCode CustomEnum { get; set; }
        }
    }
}
