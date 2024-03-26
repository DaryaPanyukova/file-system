using FileSystem.Models.Tree;

namespace FileSystem.Services.Visitors;

public class DirectoryTreeVisitor : IVisitor
{
    private int _curDepth;

    public string Result { get; private set; } = string.Empty;

    public void Visit(Folder folder)
    {
        string s = string.Concat(Enumerable.Repeat('\t', _curDepth)) + '\n';
        Result += s + folder.Name;

        _curDepth += 1;

        foreach (IDirectoryTreeNode node in folder.Files)
        {
            node.Accept(this);
        }

        _curDepth -= 1;
    }

    public void Visit(FileNode file)
    {
        Result += Enumerable.Repeat('\t', _curDepth) + file.Name + '\n';
    }
}