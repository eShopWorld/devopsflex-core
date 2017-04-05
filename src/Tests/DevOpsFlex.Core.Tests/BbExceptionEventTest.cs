namespace DevOpsFlex.Core.Tests
{
    using System;
    using DevOpsFlex.Tests.Core;
    using FluentAssertions;
    using Newtonsoft.Json;
    using Xunit;

    public class BbExceptionEventTest
    {
        [Fact, IsUnit]
        public void Ensure_ExceptionIsntSerialized()
        {
            var json = JsonConvert.SerializeObject(new BbExceptionEvent {Exception = new Exception()});
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
}
