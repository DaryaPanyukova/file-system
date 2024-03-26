using FileSystem.Entities.FileSystems;
using FileSystem.Models.Results;
using FileSystem.Services.Builders.Tree;

namespace FileSystem.Entities.Command.Tree;

public class TreeGotoCommand : ICommand
{
    public TreeGotoCommand(string? path)
    {
        Path = path ?? throw new ArgumentNullException(nameof(path));
    }

    public static TreeGotoCommandBuilder Builder => new TreeGotoCommandBuilder();

    public string Path { get; private set; }

    public Status Execute(Context context)
    {
        return context.TreeGoto(Path);
    }
}