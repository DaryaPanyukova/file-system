using FileSystem.Entities.Command.Disconnect;
using FileSystem.Models.Results;
using FileSystem.Services.StringIterators;

namespace FileSystem.Entities.Parsers.CommandParsers.Disconnect;

public class DisconnectParser : ICommandParser
{
    public ICommandParser? OtherCommandParser { get; set; }

    public Result Parse(IStringIterator input)
    {
        string? word = input.GetTerm();
        if (word is null)
        {
            return new Result(new Status.Success.EmptyCommand(), null);
        }

        Result result;
        if (word == "disconnect")
        {
            result = new Result(new Status.Success(), new DisconnectCommand());
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