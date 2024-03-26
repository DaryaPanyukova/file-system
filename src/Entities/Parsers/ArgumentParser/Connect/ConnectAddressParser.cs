using FileSystem.Entities.Command;
using FileSystem.Models.Results;
using FileSystem.Services.Builders.Connect;
using FileSystem.Services.StringIterators;

namespace FileSystem.Entities.Parsers.ArgumentParser.Connect;

public class ConnectAddressParser : IArgumentParser<ConnectCommandBuilder>
{
    public IArgumentParser<ConnectCommandBuilder>? ArgumentParser { get; set; }

    public Result Parse(IStringIterator input, ConnectCommandBuilder builder)
    {
        string? address = input.GetTerm();
        builder.WithAddress(address);
        if (address == null || ArgumentParser == null)
        {
            try
            {
                ICommand command = builder.Build();
                return new Result(new Status.Success(), command);
            }
            catch (ArgumentNullException)
            {
                return new Result(new Status.Fail.SyntaxError.CommandNotInitialized(), null);
            }
        }

        return ArgumentParser.Parse(input, builder);
    }
}