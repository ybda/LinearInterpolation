namespace LinearInterpolation.Tests;

using Xunit;

public class LinearInterpolationTests
{
    [Theory]
    [InlineData(5.0, 4.0, 20.0, -50.0, 50.0, -43.75)]
    [InlineData(4.0, 4.0, 20.0, -50.0, 50.0, -50.0)]
    [InlineData(20.0, 4.0, 20.0, -50.0, 50.0, 50.0)]
    [InlineData(12.0, 4.0, 20.0, -50.0, 50.0, 0.0)]
    [InlineData(0.0, 0.0, 10.0, 0.0, 100.0, 0.0)]
    [InlineData(10.0, 0.0, 10.0, 0.0, 100.0, 100.0)]
    [InlineData(5.0, 0.0, 10.0, 0.0, 100.0, 50.0)]
    public void Interpolate_ValidInputs_CorrectOutput(decimal input, decimal inputMin, decimal inputMax, decimal outputMin, decimal outputMax, decimal expected)
    {
        // Arrange
        var inputRange = new Range<decimal>(inputMin, inputMax);
        var outputRange = new Range<decimal>(outputMin, outputMax);

        // Act
        var result = Program.Interpolate(input, inputRange, outputRange);

        // Assert
        Assert.Equal(expected, result, precision: 2);
    }
}