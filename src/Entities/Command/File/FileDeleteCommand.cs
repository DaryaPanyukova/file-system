using FileSystem.Entities.FileSystems;
using FileSystem.Models.Results;
using FileSystem.Services.Builders.File;

namespace FileSystem.Entities.Command.File;

public class FileDeleteCommand : ICommand
{
    public FileDeleteCommand(string? path)
    {
        Path = path ?? throw new ArgumentNullException(nameof(path));
    }

    public static FileDeleteCommandBuilder Builder => new FileDeleteCommandBuilder();

    public string Path { get; private set; }

    public Status Execute(Context context)
    {
        return context.FileDelete(Path);
    }
}