using Eshopworld.Core;
using Eshopworld.Tests.Core;
using FluentAssertions;
using Xunit;

// ReSharper disable once CheckNamespace
public class AnonymousTelemetryEventTest
{
    public class ToStringDictionary
    {
        [Fact, IsUnit]
        public void Test_Flat_Class()
        {
            var payload = new
            {
                Name = "some name",
                Value = 123,
                Enum = AnonymousTestEnum.SomeValue
            };

            var anonymousEvent = new AnonymousTelemetryEvent(payload);

            var result = anonymousEvent.ToStringDictionary(EventFilterTargets.All);

            result[nameof(payload.Name)].Should().Be(payload.Name);
            result[nameof(payload.Value)].Should().Be(payload.Value.ToString());
            result[nameof(payload.Enum)].Should().Be(((int)payload.Enum).ToString());
            result[nameof(AnonymousTelemetryEvent.IsAnonymous)].Should().Be(true.ToString().ToLowerInvariant());
            result.ContainsKey(nameof(AnonymousTelemetryEvent.CallerMemberName)).Should().BeTrue();
            result.ContainsKey(nameof(AnonymousTelemetryEvent.CallerFilePath)).Should().BeTrue();
            result.ContainsKey(nameof(AnonymousTelemetryEvent.CallerLineNumber)).Should().BeTrue();
        }

        [Fact, IsUnit]
        public void Test_Reference_Class()
        {
            var payload = new
            {
                Name = "some name",
                Value = 123,
                Enum = AnonymousTestEnum.SomeValue,
                AReference = new
                {
                    AnotherName = "another name",
                    AnotherValue = 321
                }
            };

            var anonymousEvent = new AnonymousTelemetryEvent(payload);

            var result = anonymousEvent.ToStringDictionary(EventFilterTargets.All);

            result[nameof(payload.Name)].Should().Be(payload.Name);
            result[nameof(payload.Value)].Should().Be(payload.Value.ToString());
            result[nameof(payload.Enum)].Should().Be(((int)payload.Enum).ToString());
            result.ContainsKey(nameof(payload.AReference)).Should().BeFalse();
            result[nameof(AnonymousTelemetryEvent.IsAnonymous)].Should().Be(true.ToString().ToLowerInvariant());
            result.ContainsKey(nameof(AnonymousTelemetryEvent.CallerMemberName)).Should().BeTrue();
            result.ContainsKey(nameof(AnonymousTelemetryEvent.CallerFilePath)).Should().BeTrue();
            result.ContainsKey(nameof(AnonymousTelemetryEvent.CallerLineNumber)).Should().BeTrue();
        }
    }

    public enum AnonymousTestEnum
    {
        SomeValue
    }
}
