using FileSystem.Models.Results;
using FileSystem.Services.StringIterators;

namespace FileSystem.Entities.Parsers.CommandParsers;

public interface ICommandParser
{
    Result Parse(IStringIterator input);
}