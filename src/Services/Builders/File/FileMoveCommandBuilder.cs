using FileSystem.Entities.Command;
using FileSystem.Entities.Command.File;

namespace FileSystem.Services.Builders.File;

public class FileMoveCommandBuilder : ICommandBuilder
{
    private string? _sourcePath;
    private string? _destinationPath;

    public FileMoveCommandBuilder WithSourcePath(string? sourcePath)
    {
        _sourcePath = sourcePath;
        return this;
    }

    public FileMoveCommandBuilder WithDestinationPath(string? destinationPath)
    {
        _destinationPath = destinationPath;
        return this;
    }

    public ICommand Build()
    {
        return new FileMoveCommand(_sourcePath, _destinationPath);
    }
}