using FileSystem.Services.Visitors;

namespace FileSystem.Models.Tree;

public record FileNode(string Name) : IDirectoryTreeNode
{
    public void Accept(IVisitor visitor)
    {
        visitor.Visit(this);
    }
}
