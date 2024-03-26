using FileSystem.Entities.Command.Tree;
using FileSystem.Entities.Parsers.ArgumentParser;
using FileSystem.Models.Results;
using FileSystem.Services.Builders.Tree;
using FileSystem.Services.StringIterators;

namespace FileSystem.Entities.Parsers.CommandParsers.Tree;

public class TreeListParser : ICommandParser
{
    public ICommandParser? OtherCommandParser { get; set; }

    public IArgumentParser<TreeListCommandBuilder>? ArgumentParser { get; set; }

    public Result Parse(IStringIterator input)
    {
        string? word = input.GetTerm();

        if (word == null)
        {
            return new Result(new Status.Success.EmptyCommand(), null);
        }

        Result result;
        if (word == "list")
        {
            result = ArgumentParser == null
                ? new Result(new Status.Success(), new TreeListCommand(null, null))
                : ArgumentParser.Parse(input, new TreeListCommandBuilder());
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