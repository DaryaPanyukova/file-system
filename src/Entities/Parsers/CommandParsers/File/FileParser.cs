using FileSystem.Models.Results;
using FileSystem.Services.StringIterators;

namespace FileSystem.Entities.Parsers.CommandParsers.File;

public class FileParser : ICommandParser
{
    public ICommandParser? OtherCommandParser { get; set; }

    public ICommandParser? ConcreteFileCommandParser { get; set; }

    public Result Parse(IStringIterator input)
    {
        string? word = input.GetTerm();
        if (word is null)
        {
            return new Result(new Status.Success.EmptyCommand(), null);
        }

        if (word == "file")
        {
            return ConcreteFileCommandParser == null
                ? new Result(new Status.Fail.SyntaxError.UnknownCommand(), null)
                : ConcreteFileCommandParser.Parse(input);
        }
        else
        {
            input.PutTerm();
            return OtherCommandParser == null
                ? new Result(new Status.Fail.SyntaxError.UnknownCommand(), null)
                : OtherCommandParser.Parse(input);
        }
    }
}