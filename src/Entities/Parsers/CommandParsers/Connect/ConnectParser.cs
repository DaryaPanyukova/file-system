using FileSystem.Entities.Command.Connect;
using FileSystem.Entities.Parsers.ArgumentParser;
using FileSystem.Models.Results;
using FileSystem.Services.Builders.Connect;
using FileSystem.Services.StringIterators;

namespace FileSystem.Entities.Parsers.CommandParsers.Connect;

public class ConnectParser : ICommandParser
{
    public ICommandParser? OtherCommandParser { get; set; }

    public IArgumentParser<ConnectCommandBuilder>? ArgumentParser { get; set; }

    public Result Parse(IStringIterator input)
    {
        string? word = input.GetTerm();

        if (word == null)
        {
            return new Result(new Status.Success.EmptyCommand(), null);
        }

        Result result;
        if (word == "connect")
        {
            result = ArgumentParser == null
                ? new Result(new Status.Success(), new ConnectCommand(null, null))
                : ArgumentParser.Parse(input, new ConnectCommandBuilder());
        }
        else
        {
            input.PutTerm();
            result = OtherCommandParser == null
                ? new Result(new Status.Fail.SyntaxError.UnknownCommand(), null)
                : OtherCommandParser.Parse(input);
        }

        if (result.Status is Status.Success && !input.IsEnd())
        {
            result = new Result(new Status.Fail.SyntaxError.TooManyArguments(), null);
        }

        return result;
    }
}