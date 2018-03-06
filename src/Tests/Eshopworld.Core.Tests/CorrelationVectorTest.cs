using Eshopworld.Core;
using Eshopworld.Tests.Core;
using FluentAssertions;
using Xunit;

// ReSharper disable once CheckNamespace
public class CorrelationVectorTest
{
    public class Initialize
    {
        [Fact, IsUnit]
        public void Test_FromEmpty()
        {
            var vector = new CorrelationVector().Initialize();

            vector.Id.Should().NotBeNullOrEmpty();
            vector.PreviousDimensions.Should().BeEmpty();
            vector.CurrentDimension.Should().Be(1);
        }

        [Theory, IsUnit]
        [InlineData("some-id", "1")]
        [InlineData("some-id", "2.2")]
        [InlineData("some-id", "3.3.3")]
        public void Test_FromPreviousVector(string previousId, string previousDimensions)
        {
            var vector = new CorrelationVector().Initialize($"{previousId}.{previousDimensions}");

            vector.Id.Should().Be(previousId);
            vector.PreviousDimensions.Should().Be($".{previousDimensions}");
            vector.CurrentDimension.Should().Be(1);
            vector.ToString().Should().Be($"{previousId}.{previousDimensions}.1");
        }
    }
}