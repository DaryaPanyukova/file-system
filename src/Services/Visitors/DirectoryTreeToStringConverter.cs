using FileSystem.Entities.DirectoryTrees;
using FileSystem.Models.Tree;

namespace FileSystem.Services.Visitors;

public class DirectoryTreeToStringConverter : IConverter<DirectoryTree, string>
{
    public string Convert(DirectoryTree source)
    {
        var visitor = new DirectoryTreeVisitor();
        foreach (IDirectoryTreeNode node in source.Nodes)
        {
            node.Accept(visitor);
        }

        return visitor.Result;
    }
}