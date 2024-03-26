using FileSystem.Entities.Command;
using FileSystem.Entities.Command.File;

namespace FileSystem.Services.Builders.File;

public class FileCopyCommandBuilder : ICommandBuilder
{
    private string? _sourcePath;
    private string? _destinationPath;

    public FileCopyCommandBuilder WithSourcePath(string? sourcePath)
    {
        _sourcePath = sourcePath;
        return this;
    }

    public FileCopyCommandBuilder WithDestinationPath(string? destinationPath)
    {
        _destinationPath = destinationPath;
        return this;
    }

    public ICommand Build()
    {
        return new FileCopyCommand(_sourcePath, _destinationPath);
    }
}