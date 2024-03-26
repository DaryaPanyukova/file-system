using FileSystem.Entities.Command;
using FileSystem.Entities.Command.File;
using FileSystem.Entities.Outputs;

namespace FileSystem.Services.Builders.File;

public class FileShowCommandBuilder : ICommandBuilder
{
    private string? _path;
    private IOutput? _output;

    public FileShowCommandBuilder WithPath(string? path)
    {
        _path = path;
        return this;
    }

    public FileShowCommandBuilder WithOutput(IOutput? output)
    {
        _output = output;
        return this;
    }

    public ICommand Build()
    {
        return new FileShowCommand(_path, _output);
    }
}