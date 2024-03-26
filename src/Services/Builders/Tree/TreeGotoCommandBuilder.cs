using FileSystem.Entities.Command;
using FileSystem.Entities.Command.Tree;

namespace FileSystem.Services.Builders.Tree;

public class TreeGotoCommandBuilder : ICommandBuilder
{
    private string? _path;

    public TreeGotoCommandBuilder WithPath(string? path)
    {
        _path = path;
        return this;
    }

    public ICommand Build()
    {
        return new TreeGotoCommand(_path);
    }
}