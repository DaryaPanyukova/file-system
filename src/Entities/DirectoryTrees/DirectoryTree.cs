using System.Collections.ObjectModel;
using FileSystem.Models.Tree;

namespace FileSystem.Entities.DirectoryTrees;

public class DirectoryTree
{
    public Collection<IDirectoryTreeNode> Nodes { get; private set; } = new Collection<IDirectoryTreeNode>();

    public FileNode AddFile(string fileName)
    {
        var file = new FileNode(fileName);
        Nodes.Add(file);
        return file;
    }

    public Folder AddFolder(string folderName)
    {
        var folder = new Folder(folderName);
        Nodes.Add(folder);
        return folder;
    }
}