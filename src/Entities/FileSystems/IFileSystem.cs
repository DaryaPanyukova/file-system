using FileSystem.Models;
using FileSystem.Models.Results;

namespace FileSystem.Entities.FileSystems;

public interface IFileSystem
{
    Status Connect(string path);
    Status Disconnect();
    bool Exists(string path);
    StatusStringReturnType FileShow(string path);
    Status FileMove(string sourcePath, string destinationPath);
    Status FileCopy(string sourcePath, string destinationPath);
    Status FileDelete(string path);
    Status FileRename(string path, string name);
    StatusStringReturnType GetFileName(string path);
    string[] GetFiles(string path);
    string[] GetDirectories(string path);
}