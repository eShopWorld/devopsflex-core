using Eshopworld.Core;
using Eshopworld.Tests.Core;
using FluentAssertions;
using Xunit;

// ReSharper disable once CheckNamespace
public class BbAnonymousEventTest
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

            var anonymousEvent = new BbAnonymousEvent(payload);

            var result = anonymousEvent.ToStringDictionary();

            result[nameof(payload.Name)].Should().Be(payload.Name);
            result[nameof(payload.Value)].Should().Be(payload.Value.ToString());
            result[nameof(payload.Enum)].Should().Be(((int) payload.Enum).ToString());
            result[nameof(BbAnonymousEvent.IsAnonymous)].Should().Be(true.ToString().ToLowerInvariant());
            result.ContainsKey(nameof(BbAnonymousEvent.CallerMemberName)).Should().BeTrue();
            result.ContainsKey(nameof(BbAnonymousEvent.CallerFilePath)).Should().BeTrue();
            result.ContainsKey(nameof(BbAnonymousEvent.CallerLineNumber)).Should().BeTrue();
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

            var anonymousEvent = new BbAnonymousEvent(payload);

            var result = anonymousEvent.ToStringDictionary();

            result[nameof(payload.Name)].Should().Be(payload.Name);
            result[nameof(payload.Value)].Should().Be(payload.Value.ToString());
            result[nameof(payload.Enum)].Should().Be(((int)payload.Enum).ToString());
            result.ContainsKey(nameof(payload.AReference)).Should().BeFalse();
            result[nameof(BbAnonymousEvent.IsAnonymous)].Should().Be(true.ToString().ToLowerInvariant());
            result.ContainsKey(nameof(BbAnonymousEvent.CallerMemberName)).Should().BeTrue();
            result.ContainsKey(nameof(BbAnonymousEvent.CallerFilePath)).Should().BeTrue();
            result.ContainsKey(nameof(BbAnonymousEvent.CallerLineNumber)).Should().BeTrue();
        }
    }

    public enum AnonymousTestEnum
    {
        SomeValue
    }
}
