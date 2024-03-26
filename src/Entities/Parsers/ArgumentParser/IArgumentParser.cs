using FileSystem.Models.Results;
using FileSystem.Services.StringIterators;

namespace FileSystem.Entities.Parsers.ArgumentParser;

public interface IArgumentParser<T>
{
    Result Parse(IStringIterator input, T builder);
}