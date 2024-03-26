namespace FileSystem.Entities.Outputs;

public interface IOutput
{
    void Print<T>(T value);
}