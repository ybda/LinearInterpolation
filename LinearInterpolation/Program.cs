namespace LinearInterpolation;

public record Range<T>(T Min, T Max);

public static class Program
{
    private static void Main()
    {
        var currentRange = new Range<decimal>(4, 20);
        var temperatureRange = new Range<decimal>(-50, 50);

        decimal inputCurrent = 6.0m; 
        var temperature = Interpolate(inputCurrent, currentRange, temperatureRange);

        Console.WriteLine($"The temperature for {inputCurrent:F2} mA is {temperature:F2} °C.");
        
        decimal inputTemperature = -40.0m; 
        var current = Interpolate(inputTemperature, temperatureRange, currentRange);

        Console.WriteLine($"The current for {inputTemperature:F2} °C is {current:F2} mA.");
    }
    
    public static decimal Interpolate(decimal input, Range<decimal> inputRange, Range<decimal> outputRange)
    {
        decimal slope = (outputRange.Max - outputRange.Min) / (inputRange.Max - inputRange.Min);
        return (input - inputRange.Min) * slope + outputRange.Min;
    }
}