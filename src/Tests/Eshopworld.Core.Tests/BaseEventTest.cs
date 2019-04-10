using System;
using System.Collections.Generic;
using Eshopworld.Core;
using Eshopworld.Tests.Core;
using FluentAssertions;
using Moq;
using Newtonsoft.Json;
using Xunit;

// ReSharper disable once CheckNamespace
public class BaseEventTest
{
    public class ToStringDictionary
    {
        [Fact, IsUnit]
        public void Test_ConvertsTestPoco()
        {
            var poco = new TestEvent();

            var result = poco.ToStringDictionary(EventFilterTargets.ApplicationInsights);

            result[nameof(TestEvent.SomeInt)].Should().Be(poco.SomeInt.ToString());
            result[nameof(TestEvent.SomeString)].Should().Be(poco.SomeString);
            result.ContainsKey(nameof(TestEvent.BadReference)).Should().BeFalse();
        }

        [Fact, IsUnit]
        public void Test_ConvertsTestReferencePoco_WithoutReferences()
        {
            var poco = new TestReferenceEvent();

            var result = poco.ToStringDictionary(EventFilterTargets.ApplicationInsights);

            result[nameof(TestEvent.SomeInt)].Should().Be(poco.SomeInt.ToString());
            result[nameof(TestEvent.SomeString)].Should().Be(poco.SomeString);
            result.ContainsKey(nameof(TestEvent.BadReference)).Should().BeFalse();
        }

        [Fact, IsUnit]
        public void Test_ConvertsTestBadReferencePoco_ByRemovingReference()
        {
            var poco = new BadReferenceTestEvent();

            var result = poco.ToStringDictionary(EventFilterTargets.ApplicationInsights);

            result[nameof(TestEvent.SomeInt)].Should().Be(poco.SomeInt.ToString());
            result[nameof(TestEvent.SomeString)].Should().Be(poco.SomeString);
            result.ContainsKey(nameof(TestEvent.BadReference)).Should().BeFalse();
        }
    }

    public class CopyPropertiesInto
    {
        [Fact, IsUnit]
        public void Test_Copy_With_Replacement()
        {
            var existingProperties = new Dictionary<string, string>
            {
                {$"{nameof(TestEvent.SomeInt)}", "old int"}
            };

            var newProperties = new Dictionary<string, string>
            {
                {$"{nameof(TestEvent.SomeInt)}", "new int"},
                {$"{nameof(TestEvent.SomeString)}", "new string"}
            };

            var poco = new Mock<TestEvent>();

            poco.Setup(x => x.ToStringDictionary(EventFilterTargets.ApplicationInsights)).Returns(newProperties);

            poco.Object.CopyPropertiesInto(existingProperties);

            existingProperties[nameof(TestEvent.SomeInt)].Should().Be(newProperties[nameof(TestEvent.SomeInt)]);
            existingProperties[nameof(TestEvent.SomeString)].Should().Be(newProperties[nameof(TestEvent.SomeString)]);
        }

        [Fact, IsUnit]
        public void Test_Copy_Without_Replacement()
        {
            var existingProperties = new Dictionary<string, string>
            {
                {$"{nameof(TestEvent.SomeInt)}", "old int"}
            };

            var newProperties = new Dictionary<string, string>
            {
                {$"{nameof(TestEvent.SomeInt)}", "new int"},
                {$"{nameof(TestEvent.SomeString)}", "new string"}
            };

            var poco = new Mock<TestEvent>();

            poco.Setup(x => x.ToStringDictionary(EventFilterTargets.ApplicationInsights)).Returns(newProperties);

            poco.Object.CopyPropertiesInto(existingProperties, false);

            existingProperties[nameof(TestEvent.SomeInt)].Should().Be(existingProperties[nameof(TestEvent.SomeInt)]);
            existingProperties[nameof(TestEvent.SomeString)].Should().Be(newProperties[nameof(TestEvent.SomeString)]);
        }
    }

    public class TestEvent : TelemetryEvent
    {
        public TestEvent()
        {
            SomeInt = new Random().Next(100);
            SomeString = Lorem.GetSentence();
        }

        public int SomeInt { get; set; }

        public string SomeString { get; set; }

        [JsonIgnore]
        public TestEvent BadReference => this;
    }

    public class TestReferenceEvent : TelemetryEvent
    {
        public TestReferenceEvent()
        {
            SomeInt = new Random().Next(100);
            SomeString = Lorem.GetSentence();
        }

        public int SomeInt { get; set; }

        public string SomeString { get; set; }
    }

    public class BadReferenceTestEvent : TelemetryEvent
    {
        public BadReferenceTestEvent()
        {
            SomeInt = new Random().Next(100);
            SomeString = Lorem.GetSentence();
        }

        public int SomeInt { get; set; }

        public string SomeString { get; set; }

        public BadReferenceTestEvent BadReference => this;
    }
}
