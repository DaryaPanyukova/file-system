using FileSystem.Entities.FileSystems;
using FileSystem.Models.Results;
using FileSystem.Services.Builders.Connect;

namespace FileSystem.Entities.Command.Connect;

public class ConnectCommand : ICommand
{
    public ConnectCommand(string? address, IFileSystem? fileSystem)
    {
        Address = address ?? throw new ArgumentNullException(nameof(address));
        FileSystem = fileSystem ?? throw new ArgumentNullException(nameof(fileSystem));
    }

    public static ConnectCommandBuilder Builder => new ConnectCommandBuilder();
    public string Address { get; private set; }
    public IFileSystem FileSystem { get; private set; }

    public Status Execute(Context context)
    {
        return context.Connect(Address, FileSystem);
    }
}