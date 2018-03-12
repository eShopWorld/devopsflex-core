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
        }
    }

    public class Name
    {
        [Fact, IsUnit]
        public void Test_Name_ReturnsCallerName()
        {
            const string name = "some name";

            var anonymousEvent = new BbAnonymousEvent(new { }) { CallerMemberName = name };

            anonymousEvent.Name.Should().Be(name);
        }
    }

    public enum AnonymousTestEnum
    {
        SomeValue
    }
}
