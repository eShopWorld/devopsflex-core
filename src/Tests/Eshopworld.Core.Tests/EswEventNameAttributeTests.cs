using System;
using System.Collections.Generic;
using Eshopworld.Tests.Core;
using FluentAssertions;
using Xunit;

namespace Eshopworld.Core.Tests
{
    public class EswEventNameAttributeTests
    {
        [Fact, IsUnit]
        public void EventName_ValidStringProvided_ShouldSetCorrectly()
        {
            // Arrange
            var eventName = "customEventName";
            var sut = new EswEventNameAttribute(eventName);

            // Assert
            sut.EventName.Should().Be(eventName);
        }

        [Fact, IsUnit]
        public void AttributeUsageAttributes_ShouldBeSetCorrectly()
        {
            // Arrange
            var attributes = (IList<AttributeUsageAttribute>)typeof(EswEventNameAttribute)
                .GetCustomAttributes(typeof(AttributeUsageAttribute), false);

            // Assert
           attributes.Should().ContainSingle();
           attributes[0].AllowMultiple.Should().BeFalse();
           attributes[0].Inherited.Should().BeFalse();
           attributes[0].ValidOn.Should().Be(AttributeTargets.Class);
        }
    }
}
