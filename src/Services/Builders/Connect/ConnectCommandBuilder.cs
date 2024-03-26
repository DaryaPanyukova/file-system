using FileSystem.Entities.Command;
using FileSystem.Entities.Command.Connect;
using FileSystem.Entities.FileSystems;

namespace FileSystem.Services.Builders.Connect;

public class ConnectCommandBuilder : ICommandBuilder
{
    private string? _address;
    private IFileSystem? _fileSystem;

    public ConnectCommandBuilder WithAddress(string? address)
    {
        _address = address;
        return this;
    }

    public ConnectCommandBuilder WithFileSystem(IFileSystem? fileSystem)
    {
        _fileSystem = fileSystem;
        return this;
    }

    public ICommand Build()
    {
        return new ConnectCommand(_address, _fileSystem);
    }
}