using FileSystem.Models.Results;
using FileSystem.Services.StringIterators;

namespace FileSystem.Entities.Parsers.CommandParsers.Tree;

public class TreeParser : ICommandParser
{
    public ICommandParser? OtherCommandParser { get; set; }

    public ICommandParser? ConcreteTreeCommandParser { get; set; }

    public Result Parse(IStringIterator input)
    {
        string? word = input.GetTerm();
        if (word is null)
        {
            return new Result(new Status.Success.EmptyCommand(), null);
        }

        if (word == "tree")
        {
            return ConcreteTreeCommandParser == null
                ? new Result(new Status.Fail.SyntaxError.UnknownCommand(), null)
                : ConcreteTreeCommandParser.Parse(input);
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