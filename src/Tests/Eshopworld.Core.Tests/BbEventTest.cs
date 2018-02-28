using System;
using System.Diagnostics.CodeAnalysis;
using Eshopworld.Core;
using Eshopworld.Tests.Core;
using FluentAssertions;
using Newtonsoft.Json;
using Xunit;

// ReSharper disable once CheckNamespace
[ExcludeFromCodeCoverage]
public class BbEventTest
{
    public class ToStringDictionary
    {
        [Fact, IsUnit]
        public void Test_ConvertsTestPoco()
        {
            var poco = new TestEvent();

            var result = poco.ToStringDictionary();

            result[nameof(TestEvent.SomeInt)].Should().Be(poco.SomeInt.ToString());
            result[nameof(TestEvent.SomeString)].Should().Be(poco.SomeString);
            result.ContainsKey(nameof(TestEvent.BadReference)).Should().BeFalse();
        }

        [Fact, IsUnit]
        public void Test_ConvertsTestReferencePoco_WithoutReferences()
        {
            var poco = new TestReferenceEvent();

            var result = poco.ToStringDictionary();

            result[nameof(TestEvent.SomeInt)].Should().Be(poco.SomeInt.ToString());
            result[nameof(TestEvent.SomeString)].Should().Be(poco.SomeString);
            result.ContainsKey(nameof(TestEvent.BadReference)).Should().BeFalse();
        }
    }

    private class TestEvent : BbTelemetryEvent
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

    private class TestReferenceEvent : BbTelemetryEvent
    {
        public TestReferenceEvent()
        {
            SomeInt = new Random().Next(100);
            SomeString = Lorem.GetSentence();
        }

        public int SomeInt { get; set; }

        public string SomeString { get; set; }
    }
}
