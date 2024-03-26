namespace FileSystem.Entities.Outputs;

public class ConsoleOutput : IOutput
{
    public void Print<T>(T value)
    {
        Console.WriteLine(value);
    }
}