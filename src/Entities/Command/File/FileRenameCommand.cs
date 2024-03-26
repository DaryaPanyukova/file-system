using FileSystem.Entities.FileSystems;
using FileSystem.Models.Results;
using FileSystem.Services.Builders.File;

namespace FileSystem.Entities.Command.File;

public class FileRenameCommand : ICommand
{
    public FileRenameCommand(string? path, string? name)
    {
        Path = path ?? throw new ArgumentNullException(nameof(path));
        Name = name ?? throw new ArgumentNullException(nameof(name));
    }

    public static FileRenameCommandBuilder Builder => new FileRenameCommandBuilder();
    public string Path { get; private set; }
    public string Name { get; private set; }

    public Status Execute(Context context)
    {
        return context.FileRename(Path, Name);
    }
}