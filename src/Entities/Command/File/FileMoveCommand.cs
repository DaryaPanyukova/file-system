using FileSystem.Entities.FileSystems;
using FileSystem.Models.Results;
using FileSystem.Services.Builders.File;

namespace FileSystem.Entities.Command.File;

public class FileMoveCommand : ICommand
{
    public FileMoveCommand(string? sourcePath, string? destinationPath)
    {
        SourcePath = sourcePath ?? throw new ArgumentNullException(nameof(sourcePath));
        DestinationPath = destinationPath ?? throw new ArgumentNullException(nameof(destinationPath));
    }

    public static FileMoveCommandBuilder Builder => new FileMoveCommandBuilder();
    public string SourcePath { get; private set; }
    public string DestinationPath { get; private set; }

    public Status Execute(Context context)
    {
        return context.FileMove(SourcePath, DestinationPath);
    }
}