using FileSystem.Entities.Command;
using FileSystem.Entities.Command.File;

namespace FileSystem.Services.Builders.File;

public class FileDeleteCommandBuilder : ICommandBuilder
{
    private string? _path;

    public FileDeleteCommandBuilder WithPath(string? path)
    {
        _path = path;
        return this;
    }

    public ICommand Build()
    {
        return new FileDeleteCommand(_path);
    }
}