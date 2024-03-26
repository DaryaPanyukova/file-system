using FileSystem.Entities.Command;
using FileSystem.Entities.Command.File;

namespace FileSystem.Services.Builders.File;

public class FileRenameCommandBuilder : ICommandBuilder
{
    private string? _path;
    private string? _name;

    public FileRenameCommandBuilder WithPath(string? path)
    {
        _path = path;
        return this;
    }

    public FileRenameCommandBuilder WithName(string? name)
    {
        _name = name;
        return this;
    }

    public ICommand Build()
    {
        return new FileRenameCommand(_path, _name);
    }
}