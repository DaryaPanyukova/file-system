using FileSystem.Entities.Parsers.CommandParsers;
using FileSystem.Models.Results;
using FileSystem.Services.StringIterators;

namespace FileSystem.Entities.Parsers;

public class Parser
{
    public ICommandParser? FirstParser { get; set; }

    public Result Parse(IStringIterator stringIterator)
    {
        return FirstParser?.Parse(stringIterator) ?? new Result(new Status.Success.EmptyCommand(), null);
    }
}