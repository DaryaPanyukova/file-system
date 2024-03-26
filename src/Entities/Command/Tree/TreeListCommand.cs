using FileSystem.Entities.DirectoryTrees;
using FileSystem.Entities.FileSystems;
using FileSystem.Entities.Outputs;
using FileSystem.Models.Results;
using FileSystem.Services.Builders.Tree;
using FileSystem.Services.Visitors;

namespace FileSystem.Entities.Command.Tree;

public class TreeListCommand : ICommand
{
    public TreeListCommand(int? depth, IOutput? output)
    {
        Depth = depth ?? throw new ArgumentNullException(nameof(depth));
        Output = output ?? throw new ArgumentNullException(nameof(output));
    }

    public static TreeListCommandBuilder Builder => new TreeListCommandBuilder();
    public int Depth { get; private set; }
    public IOutput Output { get; private set; }

    public Status Execute(Context context)
    {
        var treeBuilder = new DirectoryTreeBuilder(context);
        DirectoryTree tree = treeBuilder.Build(Depth);
        var converter = new DirectoryTreeToStringConverter();
        string result = converter.Convert(tree);
        Output.Print(result);
        return new Status.Success();
    }
}