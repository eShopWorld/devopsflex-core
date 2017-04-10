using System;
using DevOpsFlex.Core;
using DevOpsFlex.Tests.Core;
using FluentAssertions;
using Newtonsoft.Json;
using Xunit;

// ReSharper disable once CheckNamespace
public class BbEventTest
{
    public class ToStringDictionary
    {
        [Fact, IsUnit]
        public void Test_ConvertsTestPoco()
        {
            var poco = new TestEvent
            {
                SomeInt = new Random().Next(100),
                SomeString = Lorem.GetSentence()
            };

            var result = poco.ToStringDictionary();

            result[nameof(TestEvent.SomeInt)].Should().Be(poco.SomeInt.ToString());
            result[nameof(TestEvent.SomeString)].Should().Be(poco.SomeString);
            result.ContainsKey(nameof(TestEvent.BadReference)).Should().BeFalse();
        }
    }

    private class TestEvent : BbEvent
    {
        public int SomeInt { get; set; }

        public string SomeString { get; set; }

        [JsonIgnore]
        public TestEvent BadReference => this;
    }
}
