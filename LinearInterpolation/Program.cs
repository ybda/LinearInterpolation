using Tomlyn;

namespace LinearInterpolation;

public record Range<T>(T Min, T Max) where T : new()
{
    public Range(IList<T> items) : this(items[0], items[1]) {}
    public Range() : this(new T(), new T()) {}
}

public record NamedRange<T>(string Name, Range<T> Range) where T : new()
{
    public NamedRange() : this("", new Range<T>()) {}
}


public class TomlModel
{
    public NamedRange<decimal> Range1 { get; set; }
    public NamedRange<decimal> Range2 { get; set; }
    public decimal Input { get; set; }
    public int InputRangeNumber { get; set; }
}

public static class Program
{
    private static void Main()
    {
        for (;;)
        {
            var cfg = GetTomlTableFromFile();
        
            string printString = cfg.InputRangeNumber == 1 ?
                InterpolationToString(cfg.Input, cfg.Range1, cfg.Range2) 
                : InterpolationToString(cfg.Input, cfg.Range2, cfg.Range1);
        
            Console.WriteLine($"[{DateTime.Now}] {printString}");
            
            Console.Write("> ");
            Console.ReadLine();
        }
    }

    private static TomlModel GetTomlTableFromFile()
    {
        string tomlContent = File.ReadAllText("../../../params.toml");
        return Toml.Parse(tomlContent).ToModel<TomlModel>();
    }

    private static string InterpolationToString(decimal input, 
        NamedRange<decimal> range1, 
        NamedRange<decimal> range2)
    {
        var output = Interpolate(input, range1.Range, range2.Range);
        return $"{input:F2} {range1.Name} = {output:F2} {range2.Name}";
    }
    
    public static decimal Interpolate(decimal input, Range<decimal> inputRange, Range<decimal> outputRange)
    {
        decimal slope = (outputRange.Max - outputRange.Min) / (inputRange.Max - inputRange.Min);
        return (input - inputRange.Min) * slope + outputRange.Min;
    }
}