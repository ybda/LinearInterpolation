namespace LinearInterpolation;

public record Range<T>(T Min, T Max);

public static class Program
{
    private static void Main()
    {
        var range1 = new Range<decimal>(0, 5);
        const string range1Name = "mA";
        
        var range2 = new Range<decimal>(-200, 110);
        const string range2Name = "°C";

        const decimal input = -167.0m;
        const int inputRangeNumber = 2;
        
        
        
        string printString = inputRangeNumber == 1 ?
            InterpolationToString(input, range1, range1Name, range2, range2Name) 
            : InterpolationToString(input, range2, range2Name, range1, range1Name);
        
        Console.WriteLine(printString);
    }

    private static string InterpolationToString(decimal input, 
        Range<decimal> range1, string range1Name, 
        Range<decimal> range2, string range2Name)
    {
        var output = Interpolate(input, range1, range2);
        return $"{input:F2} {range1Name} = {output:F2} {range2Name}";
    }
    
    public static decimal Interpolate(decimal input, Range<decimal> inputRange, Range<decimal> outputRange)
    {
        decimal slope = (outputRange.Max - outputRange.Min) / (inputRange.Max - inputRange.Min);
        return (input - inputRange.Min) * slope + outputRange.Min;
    }
}