using System;
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
            var vector = new CorrelationVector();
            vector.Initialize();

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
            var vector = new CorrelationVector();
            vector.Initialize($"{previousId}.{previousDimensions}");

            vector.Id.Should().Be(previousId);
            vector.PreviousDimensions.Should().Be($".{previousDimensions}");
            vector.CurrentDimension.Should().Be(1);
            vector.ToString().Should().Be($"{previousId}.{previousDimensions}.1");
        }
    }

    public class CheckInitialized
    {
        [Fact, IsUnit]
        public void Ensure_ThrowIfNotInitialized_DuringToString()
        {
            var vector = new CorrelationVector();
            var act = new Action(() => { var _ = vector.ToString(); });
            act.Should().Throw<InvalidOperationException>();
        }

        [Fact, IsUnit]
        public void Ensure_ThrowIfNotInitialized_DuringIncrement()
        {
            var vector = new CorrelationVector();
            var act = new Action(() => { vector.Increment(); });
            act.Should().Throw<InvalidOperationException>();
        }

        [Fact, IsUnit]
        public void Ensure_ThrowIfNotInitialized_DuringAugment()
        {
            var vector = new CorrelationVector();
            var act = new Action(() => { vector.Augment(); });
            act.Should().Throw<InvalidOperationException>();
        }
    }
}