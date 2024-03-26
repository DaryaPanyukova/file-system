using FileSystem.Entities.FileSystems;
using FileSystem.Models.Results;

namespace FileSystem.Entities.Command.Disconnect;

public class DisconnectCommand : ICommand
{
    public Status Execute(Context context)
    {
       return context.Disconnect();
    }
}