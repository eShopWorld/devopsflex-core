using System;
using System.ComponentModel.DataAnnotations;
using Eshopworld.Core;
using Eshopworld.Tests.Core;
using FluentAssertions;
using Newtonsoft.Json;
using Xunit;

// ReSharper disable once CheckNamespace
public class EventFilterTest
{
    [Fact, IsUnit]
    public void Test_FilteredOutProperty_IsOut()
    {
        var testEvent = new TestFilterEvent { SomeReference = new ReferencePoco() };
        var json = JsonConvert.SerializeObject(
            testEvent,
            new JsonSerializerSettings
            {
                ContractResolver = new EventContractResolver(EventFilterTargets.Messaging)
            });

        var result = JsonConvert.DeserializeObject<TestFilterEvent>(json);

        result.SomeInt.Should().Be(testEvent.SomeInt);
        result.SomeString.Should().Be(testEvent.SomeString);
        result.MessagingFilteredProperty.Should().NotBe(testEvent.MessagingFilteredProperty);
        result.SomeReference.Should().NotBeNull();
    }

    [Fact, IsUnit]
    public void Test_FilteredInProperty_IsIn()
    {
        var testEvent = new TestFilterEvent { SomeReference = new ReferencePoco()};
        var json = JsonConvert.SerializeObject(
            testEvent,
            new JsonSerializerSettings
            {
                ContractResolver = new EventContractResolver(EventFilterTargets.ApplicationInsights)
            });

        var result = JsonConvert.DeserializeObject<TestFilterEvent>(json);

        result.SomeInt.Should().Be(testEvent.SomeInt);
        result.SomeString.Should().Be(testEvent.SomeString);
        result.MessagingFilteredProperty.Should().Be(testEvent.MessagingFilteredProperty);
        result.SomeReference.Should().NotBeNull();
    }

    [Fact, IsUnit]
    public void Test_ReferencesAreOut_WhenIgnoreReferences()
    {

    }
}

public class TestFilterEvent : TelemetryEvent
{
    private static readonly Random Rng = new Random();

    public TestFilterEvent()
    {
        SomeInt = Rng.Next(100);
        SomeString = Lorem.GetSentence();
        MessagingFilteredProperty = Lorem.GetSentence();
    }

    public int SomeInt { get; set; }

    public string SomeString { get; set; }

    [Required]
    [EventFilter(EventFilterTargets.AllExceptMessaging)]
    [MaxLength(int.MaxValue)]
    public string MessagingFilteredProperty { get; set; }

    public ReferencePoco SomeReference { get; set; }
}

public class ReferencePoco { }